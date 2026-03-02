public class EnemyFirstIdleState : IdleState
{
    public EnemyFirstIdleState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        IdleStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyFirstPlayerDetectedState>();
        }
        else if(isPlayerInMaxAgroRange)
        {
            fsm.ChangeState<EnemyFirstPlayerDetectedState>();
        }
        else if(isIdleTimeOver)
        {
            fsm.ChangeState<EnemyFirstMoveState>();
        }
    }
}