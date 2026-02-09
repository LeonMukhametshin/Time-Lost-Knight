public class EnemyFirstStanState : StanState
{
    private EnemyFirst enemy;

    public EnemyFirstStanState(FSM fsm, Entity entity, string animBoolName, StunStateData data, EnemyFirst enemy) : base(fsm, entity, animBoolName, data)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if(isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                fsm.SetState(enemy.meleeAttackState);
            }
            else if(isPlayerInMinAgroRange)
            {
                fsm.SetState(enemy.chargeState);
            }
            else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                fsm.SetState(enemy.lookForPlayerState);
            }
        } 
    }
}