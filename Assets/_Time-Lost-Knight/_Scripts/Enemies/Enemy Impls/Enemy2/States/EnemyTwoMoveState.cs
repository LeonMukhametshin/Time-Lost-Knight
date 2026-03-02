public class EnemyTwoMoveState : MoveState
{
    public EnemyTwoMoveState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, MoveStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyTwoPlayerDetectedState>();
        }
        else if(isPlayerInMaxAgroRange)
        {
            fsm.ChangeState<EnemyTwoPlayerDetectedState>();
        }
        else if (isDetactingWall || !isDetactingLedge)
        {
            fsm.GetState<EnemyTwoIdleState>().SetFlipAfterIdle(true);
            fsm.ChangeState<EnemyTwoIdleState>();
        }
    }
}