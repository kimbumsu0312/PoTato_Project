using UnityEngine;
using UnityEngine.InputSystem;

public class Monster : Character
{
    public Vector2 inputVec;
    public Rigidbody2D target;
    Rigidbody2D rigid;
    SpriteRenderer spriter;


    public void Init()
    {
        // 체력 초기화
        isDead = false;
        // 상태 초기화
    }

    public void ResetState()
    {
        // 코루틴도 초기화 해야하는 학습 후 정의 하자.
        isDead = false;
        gameObject.SetActive(false);
    }

    public void SetTarget(Rigidbody2D target)
    {
        this.target = target;
    }

    void Awake()
    {
        //isDead = true;
        rigid = GetComponent <Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(isDead)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if(isDead)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
}
