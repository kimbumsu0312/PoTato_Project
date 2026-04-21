using UnityEngine;

public class PlayerDash : ICharacterState
{
    Player player;
    float timer;
    Vector2 dir;

    public void Enter(Character character)
    {
        this.player = character as Player;
        if (this.player == null)
        {
            Debug.LogError("Player 캐스팅 실패");
        }
        timer = player.dashDuration;

        dir = player.lastMoveDir.sqrMagnitude > 0.001f 
            ? player.lastMoveDir 
            : (player.spriterRd.flipX ? Vector2.left : Vector2.right);
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            player.stateMachine.ChangeState<PlayerIdle>();
        }
    }

    public void FixedUpdate()
    {
        player.rigid.MovePosition(
            player.rigid.position + dir * player.dashSpeed * Time.fixedDeltaTime
        );
    }

    public void Exit() { }
}
