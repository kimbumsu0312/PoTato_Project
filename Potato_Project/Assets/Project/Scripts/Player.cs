using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Character
{
    public PlayerStateMachine stateMachine;
    public Vector2 inputVec;
    public Vector2 lastMoveDir;

    public Rigidbody2D rigid;
    public SpriteRenderer spriterRd;
    Animator anim;
    public Weapon equipWeapon;

    public bool isDash;

    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterRd = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        stateMachine = new PlayerStateMachine(this);

        stateMachine.AddState(new PlayerIdle());
        stateMachine.AddState(new PlayerMove());
        stateMachine.AddState(new PlayerDash());
        stateMachine.AddState(new PlayerAttack());
    }
    void Start()
    {
        stateMachine.Init<PlayerIdle>();
    }
    void Update()
    {
        stateMachine.Update();

        if (isDead)
        {
            anim.SetTrigger("Dead");
        }
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();

    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        if (inputVec.sqrMagnitude > 0.01f)
        {
            lastMoveDir = inputVec.normalized;
        }
    }
    
    void OnAttack(InputValue value)
    {
        if (!value.isPressed){
            return;
        }
        stateMachine.ChangeState<PlayerAttack>();
    }

    void OnJump(InputValue value)
    {
        if (!value.isPressed || equipWeapon.isAttacking) {
            return;
        }
        stateMachine.ChangeState<PlayerDash>();
    }

}
