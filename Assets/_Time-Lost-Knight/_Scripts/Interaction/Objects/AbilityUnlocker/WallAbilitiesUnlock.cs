using UnityEngine;

public class WallAbilitiesUnlock : MonoBehaviour
{
    private PlayerFSM m_fsm;

    private void Start()
    {
        m_fsm = ServiceLocator.Get<Player>().fsm as PlayerFSM;
    }

    public void Unlock()
    {
        if (m_fsm is null ||
            m_fsm.GetState<PlayerWallSlideState>().active)
        {
            return;
        }

        m_fsm.ActivateState<PlayerWallGrabState>();
        m_fsm.ActivateState<PlayerWallClimbState>();
        m_fsm.ActivateState<PlayerWallSlideState>();
        m_fsm.ActivateState<PlayerWallJumpState>();

        Destroy(gameObject);
    }
}
