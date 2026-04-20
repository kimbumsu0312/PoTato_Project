using System;
using System.Collections.Generic;
public class PlayerStateMachine
{
    Dictionary<Type, IPlayerState> states = new Dictionary<Type, IPlayerState>();
    IPlayerState curState;
    Player player;

    public PlayerStateMachine(Player player)
    {
        this.player = player;   
    }
    
    public void AddState(IPlayerState state)
    {
        states[state.GetType()] = state;
    }

    public void Init<T>() where T : IPlayerState
    {
        curState = states[typeof(T)];
        curState.Enter(player);
    }

    public void ChangeState<T>() where T : IPlayerState
    {
        curState?.Exit();

        curState = states[typeof(T)];
        curState.Enter(player);
    }

    public void Update() => curState.Update();
    public void FixedUpdate() => curState.FixedUpdate();
}
