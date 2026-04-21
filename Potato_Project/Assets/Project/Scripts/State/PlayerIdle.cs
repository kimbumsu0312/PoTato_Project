using UnityEngine;

public class PlayerIdle : ICharacterState
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
        if(player.inputVec.magnitude > 0.1f)
            player.stateMachine.ChangeState<PlayerMove>();
    }

    public void FixedUpdate() { }
    public void Exit() { }
}
