public class EnemyTwoStunState : StanState
{
    public EnemyTwoStunState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity, 
        StunStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if (!isStunTimeOver)
        {
            return;
        }

        if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyTwoPlayerDetectedState>();
        }
        else
        {
            fsm.ChangeState<EnemyTwoLookForPlayerState>();
        }
    }
}