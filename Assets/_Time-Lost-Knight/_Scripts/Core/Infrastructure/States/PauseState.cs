using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.Infrastructure.States
{
    [MovedFrom("")]
    public class PauseState : IState
    {
        private StateMachine m_stateMachine;

        public PauseState(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}
