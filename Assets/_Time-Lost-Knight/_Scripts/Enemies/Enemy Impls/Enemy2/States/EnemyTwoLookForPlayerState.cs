public class EnemyTwoLookForPlayerState : LookForPlayerState
{
    public EnemyTwoLookForPlayerState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity,
        LookForPlayerStateData data) 
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
        else if(isAllTurnsTimeDone)
        {
            fsm.ChangeState<EnemyTwoMoveState>();
        }
    }
}