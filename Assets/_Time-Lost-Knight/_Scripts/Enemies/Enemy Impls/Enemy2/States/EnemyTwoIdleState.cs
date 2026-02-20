public class EnemyTwoIdleState : IdleState
{
    public EnemyTwoIdleState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity, IdleStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyTwoPlayerDetectedState>();
        }
        else if (isIdleTimeOver)
        {
            fsm.ChangeState<EnemyTwoMoveState>();
        }
    }
}