using UnityEngine;

public class OmniDashAbilityUnlock : MonoBehaviour
{
    private PlayerFSM m_fsm;

    private void Start()
    {
        m_fsm = ServiceLocator
            .Get<IPlayerFactory>()
            .Create().fsm 
            as PlayerFSM;
    }

    public void Unlock()
    {
        if (m_fsm is null ||
            m_fsm.GetState<PlayerOmnidirectionalDashState>().active)
        {
            return;
        }

        m_fsm.ActivateState<PlayerOmnidirectionalDashState>();

        Destroy(gameObject);
    }
}