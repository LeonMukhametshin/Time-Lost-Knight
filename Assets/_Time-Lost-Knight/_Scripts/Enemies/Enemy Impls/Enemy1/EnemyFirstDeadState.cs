public class EnemyFirstDeadState : DeadState
{
    private EnemyFirst m_enemy;

    public EnemyFirstDeadState(FSM fsm, Entity entity, string animBoolName, DeadStateData data, EnemyFirst enemy) : base(fsm, entity, animBoolName, data)
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
