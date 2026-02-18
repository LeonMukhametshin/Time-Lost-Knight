using UnityEngine;

public class AnimationToFSM : MonoBehaviour
{
    public AttackState attackState;

    public void Initialize(AttackState attackState)
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
