using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM
{
    [MovedFrom("")]
    public class PlayerFSM : EntityFSM
    {
        public override void ChangeState<T>()
        {
            var state = m_states[typeof(T)];

            if (state is PlayerState playerState)
            {
                if (playerState.isUnlocked)
                {
                    base.ChangeState<T>();
                }
            }
        }

        private bool TryGetState<T>(out T state) where T : PlayerState
        {
            if (m_states.TryGetValue(typeof(T), out var baseState) && baseState is T typedState)
            {
                state = typedState;
                return true;
            }
            state = null;
            return false;
        }

        public void UnlockState<T>() where T : PlayerState
        {
            if (TryGetState<T>(out var state) && !state.isUnlocked)
                state.Unlock();
        }

        public bool IsStateUnlocked<T>() where T : PlayerState =>
            TryGetState<T>(out var state) && state.isUnlocked;

    }
}
