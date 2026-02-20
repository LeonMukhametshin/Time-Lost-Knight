using UnityEngine;

public class AnimationToFSM : MonoBehaviour
{
    public IAnimationAttackTrigger attackState;

    public void Initialize(IAnimationAttackTrigger attackState)
    {
        this.attackState = attackState;
    }

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
