using System.Collections.Generic;

public class EntityFSM : StateMachine
{
    public void Update()
    {
        if (m_currentState is IUpdateState updateState)
        {
            updateState.Update();
        }
    }

    public void FixedUpdate()
    {
        if (m_currentState is IFixedUpdateState fixedUpdateState)
        {
            fixedUpdateState.FixedUpdate();
        }
    }

    public T GetState<T>() where T : EntityState
        => m_states.GetValueOrDefault(typeof(T)) as T;
}