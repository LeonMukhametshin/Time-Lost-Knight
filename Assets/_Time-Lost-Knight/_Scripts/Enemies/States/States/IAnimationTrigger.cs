using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States
{
    [MovedFrom("")]
    public interface IAnimationTrigger
    {
        void TriggerAnimation();
        void FinishAnimation();
    }
}
