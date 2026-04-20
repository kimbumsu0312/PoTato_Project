using UnityEngine;

public class PlayerAttack : IPlayerState
{
    Player player;

    public void Enter(Player player)
    {
        this.player = player;

        if (!player.equipWeapon.CanAttack())
        {
            player.stateMachine.ChangeState<PlayerIdle>();
            return;
        }
        player.equipWeapon.Attack();
    }
    public void Update()
    {
        if (player.equipWeapon.IsAttackFinished())
        {
            if (player.inputVec.magnitude > 0.1f)
                player.stateMachine.ChangeState<PlayerMove>();
            else
                player.stateMachine.ChangeState<PlayerIdle>();
        }
    }
    public void FixedUpdate(){ }
    public void Exit() { }
}
