using UnityEngine;

public class AnimationToFSM : MonoBehaviour
{
    private EntityFSM m_animationState;

    public void Initialize(EntityFSM attackState)
    {
        this.m_animationState = attackState;
    }

    private void TriggerAnimation()
    {
        var a = m_animationState.currentState as IAnimationTrigger;
        a.TriggerAnimation();
    }

    private void FinishAnimation()
    {
        var a = m_animationState.currentState as IAnimationTrigger;
        a.FinishAnimation();
    }
}