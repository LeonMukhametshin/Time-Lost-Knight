using Game.Player.FSM.States.Impls;

public class DashAbilityUnlock : AbilityUnlock
{
    public override void Unlock()
    {
        if (m_playerFSM is null)
        {
            return;
        }

        if (m_playerFSM.IsStateUnlocked<PlayerForwardDashState>())
        {
            Destroy(gameObject);
            return;
        }

        m_playerFSM.UnlockState<PlayerForwardDashState>();
        PlayUnlockAudio();
        Destroy(gameObject);
    }
}
