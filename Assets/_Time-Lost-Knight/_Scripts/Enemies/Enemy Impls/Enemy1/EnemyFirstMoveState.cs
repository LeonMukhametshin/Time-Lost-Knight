public class EnemyFirstMoveState : MoveState
{
    public EnemyFirstMoveState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        MoveStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyFirstIdleState>();
        }
        else if(isDetactingWall || !isDetactingLedge)
        {
            fsm.GetState<EnemyFirstIdleState>().SetFlipAfterIdle(true);
            fsm.ChangeState<EnemyFirstIdleState>();
        }
    }
}