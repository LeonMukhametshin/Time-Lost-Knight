public class EnemyFourPlayerDetectedState : PlayerDetectedState
{
    public EnemyFourPlayerDetectedState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, PlayerDetectedData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (performeLongRangeAction)
        {
            fsm.ChangeState<EnemyFourAttackState>();
        }
        else if (!isPlayerInMaxAgroRange)
        {
            fsm.ChangeState<EnemyFourLookForPlayerState>();
        }
    }
}
