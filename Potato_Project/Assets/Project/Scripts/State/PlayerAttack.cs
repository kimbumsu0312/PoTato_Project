using UnityEngine;

public class PlayerAttack : ICharacterState
{
    Player player;

    public void Enter(Character character)
    {
        this.player = character as Player;
        if (this.player == null)
        {
            Debug.LogError("Player 캐스팅 실패");
        }
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
