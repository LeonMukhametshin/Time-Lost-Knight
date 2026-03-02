using UnityEngine;

public class EnemyThreeRangeAttackState : RangeAttackState
{
    public EnemyThreeRangeAttackState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, Transform attackPosition, RangeAttackData data) 
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

        if(isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyThreePlayerDetectedState>();
        }
        else
        {
            fsm.ChangeState<EnemyThreeLookForPlayerState>();
        }
    }
}