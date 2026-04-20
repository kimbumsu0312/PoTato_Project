using UnityEngine;

public class PlayerIdle : IPlayerState
{
    Player player;

    public void Enter(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        if(player.inputVec.magnitude > 0.1f)
            player.stateMachine.ChangeState<PlayerMove>();
    }

    public void FixedUpdate() { }
    public void Exit() { }
}
