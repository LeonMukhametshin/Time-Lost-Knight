public class EnemyFirstIdleState : IdleState
{
    private EnemyFirst m_enemy;

    public EnemyFirstIdleState(FSM fsm, Entity entity, string animBoolName, IdleStateData data, EnemyFirst enemy) : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
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
        else if(isIdleTimeOver)
        {
            fsm.SetState(m_enemy.moveState);
        }
    }
}
