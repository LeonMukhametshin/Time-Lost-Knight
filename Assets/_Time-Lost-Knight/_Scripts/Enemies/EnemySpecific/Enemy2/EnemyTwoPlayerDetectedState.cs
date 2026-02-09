public class EnemyTwoPlayerDetectedState : PlayerDetectedState
{
    private EnemyTwo m_enemy;

    public EnemyTwoPlayerDetectedState(FSM fsm, Entity entity, string animBoolName, PlayerDetectedData data, EnemyTwo enemy) : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
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

    public override void Update()
    {
        base.Update();

        if(performeCloseRangeAction)
        {
            fsm.SetState(m_enemy.meleeAttackState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
