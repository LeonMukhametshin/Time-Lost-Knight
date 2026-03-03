public class EnemyFirstPlayerDetectedState : PlayerDetectedState
{
    protected FlipContoller flipController => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    private FlipContoller m_flipContoller;

    public EnemyFirstPlayerDetectedState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        PlayerDetectedData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (performeCloseRangeAction)
        {
            fsm.ChangeState<EnemyFirstMeleeAttackState>();
        }
        else if (performeLongRangeAction)
        {
            fsm.ChangeState<EnemyFirstChargeState>();
        }
        else if(!isPlayerInMaxAgroRange)
        {
            fsm.ChangeState<EnemyFirstLookForPlayerState>();
        }
        else if(!isDetectingLedge)
        {
            flipController.Flip();
            fsm.ChangeState<EnemyFirstMoveState>();
        }
    } 
}