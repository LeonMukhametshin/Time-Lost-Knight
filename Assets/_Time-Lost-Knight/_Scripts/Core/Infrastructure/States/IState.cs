using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.Infrastructure.States
{
    [MovedFrom("")]
    public interface IState
    {
        void Enter();
        void Exit();
    }
}
