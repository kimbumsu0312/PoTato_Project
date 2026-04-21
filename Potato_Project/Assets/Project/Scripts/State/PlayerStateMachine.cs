using System;
using System.Collections.Generic;
public class PlayerStateMachine
{
    Dictionary<Type, ICharacterState> states = new Dictionary<Type, ICharacterState>();
    ICharacterState curState;
    Player player;

    public PlayerStateMachine(Player player)
    {
        this.player = player;   
    }
    
    public void AddState(ICharacterState state)
    {
        states[state.GetType()] = state;
    }

    public void Init<T>() where T : ICharacterState
    {
        curState = states[typeof(T)];
        curState.Enter(player);
    }

    public void ChangeState<T>() where T : ICharacterState
    {
        curState?.Exit();

        curState = states[typeof(T)];
        curState.Enter(player);
    }

    public void Update() => curState.Update();
    public void FixedUpdate() => curState.FixedUpdate();
}
