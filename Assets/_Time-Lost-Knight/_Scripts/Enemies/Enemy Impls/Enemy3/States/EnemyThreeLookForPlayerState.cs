public class EnemyThreeLookForPlayerState : LookForPlayerState
{
    public EnemyThreeLookForPlayerState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, LookForPlayerStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
        {
            fsm.ChangeState<EnemyThreePlayerDetectedState>();
        }
        else if (isAllTurnsTimeDone)
        {
            fsm.ChangeState<EnemyThreeIdleState>();
        }
    }
}