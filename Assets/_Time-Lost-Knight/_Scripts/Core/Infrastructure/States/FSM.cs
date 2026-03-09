using System;
using System.Collections.Generic;

public class StateMachine
{
    public IState currentState => m_currentState;
    protected IState m_currentState;

    protected Dictionary<Type, IState> m_states = new();

    public void Initialize(params IState[] stetes)
    {
        if(m_states.Count > 0)
        {
            return;
        }

        foreach(var state in stetes)
        {
            if(m_states.ContainsValue(state))
            {
                //TODO: exception 
                throw new ArgumentException();
            }
            m_states.Add(state.GetType(), state);
        }
    }

    public virtual void ChangeState<T>() where T : IState
    {
        m_currentState?.Exit();
        m_currentState = m_states[typeof(T)];
        m_currentState?.Enter();
    }
}