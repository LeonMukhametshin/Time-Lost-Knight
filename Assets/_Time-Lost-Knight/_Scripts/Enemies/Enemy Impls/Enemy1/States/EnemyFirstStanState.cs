public class EnemyFirstStanState : StanState
{
    public EnemyFirstStanState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        StunStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(!isStunTimeOver)
        {
            return; 
        }

        if (performCloseRangeAction)
        {
            fsm.ChangeState<EnemyFirstMeleeAttackState>();
        }
        else if (isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyFirstChargeState>();
        }
        else
        {
            fsm.GetState<EnemyFirstLookForPlayerState>().SetTurnImmediately(true);
            fsm.ChangeState<EnemyFirstLookForPlayerState>();
        }
    }
}