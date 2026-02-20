using UnityEngine;

public class EnemyTwoMeleeAttackState : MeleeAttackState
{
    public EnemyTwoMeleeAttackState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity, 
        Transform attackPosition, MeleeAttackStateData data) 
        : base(fsm, core, animBoolName, entity, attackPosition, data)
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