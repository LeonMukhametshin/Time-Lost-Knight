using UnityEngine;

public class EnemyFirstMeleeAttackState : MeleeAttackState
{
    public EnemyFirstMeleeAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        Transform attackPosition, MeleeAttackStateData data) 
        : base(fsm, core, animBoolName, entity, attackPosition, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(!isAnimationFinished)
        {
            return;
        }

        if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyFirstPlayerDetectedState>();
        }
        else
        {
            fsm.ChangeState<EnemyFirstLookForPlayerState>();
        }
    }
}