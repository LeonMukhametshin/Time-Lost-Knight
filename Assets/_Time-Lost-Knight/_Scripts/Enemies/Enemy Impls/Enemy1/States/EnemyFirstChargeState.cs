public class EnemyFirstChargeState : ChargeState
{
    public EnemyFirstChargeState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity, 
        ChargeStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (performCloseRangeAction)
        {
            fsm.ChangeState<EnemyFirstMeleeAttackState>();
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            fsm.ChangeState<EnemyFirstLookForPlayerState>();
        }
        else if (isChargeTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyFirstPlayerDetectedState>();
            }
            else
            {
                fsm.ChangeState<EnemyFirstLookForPlayerState>();
            }
        } 
    }
}
