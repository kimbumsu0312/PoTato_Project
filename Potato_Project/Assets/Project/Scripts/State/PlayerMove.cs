using UnityEngine;

public class PlayerMove : ICharacterState
{
    Player player;
    public void Enter(Character character)
    {
        this.player = character as Player;
        if (this.player == null)
        {
            Debug.LogError("Player 캐스팅 실패");
        }
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
