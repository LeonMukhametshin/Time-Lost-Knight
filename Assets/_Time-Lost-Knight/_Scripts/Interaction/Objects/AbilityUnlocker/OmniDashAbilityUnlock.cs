public class OmniDashAbilityUnlock : AbilityUnlock
{
    public override void Unlock()
    {
        if (m_playerFSM == null)
        {
            return;
        }

        if (m_playerFSM.IsStateUnlocked<PlayerOmnidirectionalDashState>())
        {
            Destroy(gameObject);
            return;
        }

        m_playerFSM.UnlockState<PlayerOmnidirectionalDashState>();
        PlayUnlockAudio();
        Destroy(gameObject);
    }
}
