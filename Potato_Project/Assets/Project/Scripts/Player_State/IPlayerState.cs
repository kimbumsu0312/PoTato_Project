using UnityEngine;

public interface IPlayerState
{
    void Enter(Player player);
    void Update();
    void FixedUpdate();
    void Exit();
}
