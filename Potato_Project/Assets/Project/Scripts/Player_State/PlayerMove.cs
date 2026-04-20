using UnityEngine;

public class PlayerMove : IPlayerState
{
    Player player;
    public void Enter(Player player)
    {
        this.player = player;
    }
    public void Update()
    {
        if (player.inputVec.magnitude < 0.1f)
            player.stateMachine.ChangeState<PlayerIdle>();
    }
    public void FixedUpdate()
    {
        player.rigid.MovePosition(
            player.rigid.position + player.Move(player.inputVec)
        );
        player.lastMoveDir = player.inputVec;
    }
    public void Exit() { }
}
