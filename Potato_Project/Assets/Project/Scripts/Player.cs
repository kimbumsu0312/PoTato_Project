using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Character
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer spriterRd;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterRd = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(isDead)
        {
            anim.SetTrigger("Dead");
        }
    }
    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Move(inputVec));
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        if(inputVec.x != 0)
        {
            spriterRd.flipX = inputVec.x < 0;
        }
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    
}
