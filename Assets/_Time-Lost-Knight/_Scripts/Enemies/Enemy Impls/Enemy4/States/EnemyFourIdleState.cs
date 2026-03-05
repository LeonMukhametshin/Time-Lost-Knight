public class EnemyFourIdleState : IdleState
{
    public EnemyFourIdleState(EntityFSM fsm, Core core, 
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
            fsm.ChangeState<EnemyFourAttackState>();
        }
        else if(isPlayerInMaxAgroRange)
        {
            fsm.ChangeState<EnemyFourLookForPlayerState>();
        }
        else if(isIdleTimeOver)
        {
            fsm.ChangeState<EnemyFourMoveState>();
        }
    }
}
