using UnityEngine;

public class AnimationToFSM : MonoBehaviour
{
    public IAnimationTrigger attackState;

    public void Initialize(IAnimationTrigger attackState)
    {
        this.attackState = attackState;
    }

    private void TriggerAttack()
    {
        attackState.TriggerAnimation();
    }

    private void FinishAttack()
    {
        attackState.FinishAnimation();
    }
}
