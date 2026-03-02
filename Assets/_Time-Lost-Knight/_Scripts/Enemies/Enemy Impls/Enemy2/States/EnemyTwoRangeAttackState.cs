using UnityEngine;

public class EnemyTwoRangeAttackState : RangeAttackState
{
    public EnemyTwoRangeAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        Transform attackPosition, RangeAttackData data) 
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
            fsm.ChangeState<EnemyTwoPlayerDetectedState>();
        }
        else
        {
            fsm.ChangeState<EnemyTwoLookForPlayerState>();
        }
    }
}