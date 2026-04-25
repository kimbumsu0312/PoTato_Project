using UnityEngine;
 using System;

public class Monster : Character
{
    public event Action<Monster> deadEvent; // 죽었을 때 이벤트
    public string poolKey;
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
        deadEvent = null;
        gameObject.SetActive(false);
    }
    public void SetTarget(Rigidbody2D target)
    {
        Debug.Log(target);
        this.target = target;
    }

    public void Die()
    {
        if(isDead)
            return;
        
        isDead = true;
        deadEvent?.Invoke(this);
        PoolManager.Instance.Return(poolKey, this);
    }

    void Awake()
    {
        //isDead = true;
        rigid = GetComponent <Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(isDead || target == null)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if(isDead || target == null)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
}
