public class WallAbilitiesUnlock : AbilityUnlock
{
    public override void Unlock()
    {
        if (m_playerFSM == null)
        {
            return;
        }

        if (m_playerFSM.IsStateUnlocked<PlayerWallGrabState>())
        {
            Destroy(gameObject);
            return;
        }

        m_playerFSM.UnlockState<PlayerWallSlideState>();
        m_playerFSM.UnlockState<PlayerWallClimbState>(); 
        m_playerFSM.UnlockState<PlayerWallGrabState>();
        m_playerFSM.UnlockState<PlayerWallJumpState>();

        Destroy(gameObject);
    }
}
