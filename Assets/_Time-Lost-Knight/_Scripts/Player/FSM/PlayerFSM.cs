using UnityEngine;

public class PlayerFSM : EntityFSM
{
    public override void ChangeState<T>()
    {
        var state = m_states[typeof(T)];

        if(state is PlayerState playerState)
        {
            if(playerState.active)
            {
                base.ChangeState<T>();
            }
            else
            {
                Debug.Log(state.ToString() + "не активирован"); 
            }
        }
    }

    public void ActivateState<T>() where T : PlayerState
    {
        var state = m_states[typeof(T)];

        if (state is PlayerState playerState)
        {
            if (!playerState.active)
            {
                playerState.active = true;
            }
        }
    }
}