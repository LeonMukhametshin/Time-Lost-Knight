public class EnemyFirstLookForPlayerState : LookForPlayerState
{
    private EnemyFirst m_enemy;

    public EnemyFirstLookForPlayerState(FSM fsm, Entity entity, string animBoolName, LookForPlayerStateData data, EnemyFirst enemyFirst) : base(fsm, entity, animBoolName, data)
    {
        this.m_enemy = enemyFirst;
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

        if(isPlayerInMinAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
        else if(isAllTurnsTimeDone)
        {
            fsm.SetState(m_enemy.moveState);
        }
    }
}
