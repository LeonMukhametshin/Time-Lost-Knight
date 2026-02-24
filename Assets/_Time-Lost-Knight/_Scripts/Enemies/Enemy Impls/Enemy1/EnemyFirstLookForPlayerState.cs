public class EnemyFirstLookForPlayerState : LookForPlayerState
{
    public EnemyFirstLookForPlayerState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity,
        LookForPlayerStateData data) 
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
        else if(isAllTurnsTimeDone)
        {
            fsm.ChangeState<EnemyFirstMoveState>();
        }
    }
}
