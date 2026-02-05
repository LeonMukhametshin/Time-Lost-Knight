using Inputs;
using System;
using System.Collections.Generic;

public class MovementStateMachine
{
    public event Action<MovementState> stateChanged;

    public MovementState currentState 
    {
        get => m_currentState;
        set
        {
            if(m_currentState != value)
            {
                m_currentState = value;
                stateChanged?.Invoke(m_currentState);
            }
        }
    }

    private MovementState m_currentState;

    private Dictionary<Type, MovementState> m_states = new();

    public void AddState(MovementState state)
    {
        if(m_states.ContainsKey(state.GetType()))
        {
            throw new Exception($"FSM also contains state: {state.GetType()}");
        }

        m_states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : MovementState
    {
        var type = typeof(T);

        if (!m_states.TryGetValue(type, out var newState))
        {
            return;
        }

        if (ReferenceEquals(currentState, newState))
        {
            return;
        }

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public T GetState<T>() where T : MovementState
    {
        var type = typeof(T);

        if(!m_states.TryGetValue(type, out var state))
        {
            throw new InvalidOperationException($"State {type.Name} not found in FSM");
        }

        return (T)state;
    }

    public void FixedUpdate() =>
        currentState?.FixedUpdate();
}