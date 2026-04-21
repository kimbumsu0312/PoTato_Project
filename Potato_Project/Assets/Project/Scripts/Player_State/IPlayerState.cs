using UnityEngine;

public interface ICharacterState
{
    void Enter(Player player);
    void Update();
    void FixedUpdate();
    void Exit();
}
