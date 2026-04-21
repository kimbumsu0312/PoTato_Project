using UnityEngine;

public interface ICharacterState
{
    void Enter(Character character);
    void Update();
    void FixedUpdate();
    void Exit();
}
