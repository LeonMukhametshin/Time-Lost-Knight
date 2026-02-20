using UnityEngine;

public class EnemyTwoMeleeAttackState : MeleeAttackState
{
    public EnemyTwoMeleeAttackState(float startTime, string animBoolName,
        Entity entity, Transform attackPosition, MeleeAttackStateData data)
        : base(startTime, animBoolName, entity, attackPosition, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (!isAnimationFinished)
        {
            return;
        }

        if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState<PlayerDetectedState>();
        }
        else if (!isPlayerInMinAgroRange)
        {
            fsm.ChangeState<LookForPlayerState>();
        }
    }
}