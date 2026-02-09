public class EnemyTwoDeadState : DeadState
{
    private EnemyTwo m_enemy;

    public EnemyTwoDeadState(FSM fsm, Entity entity, string animBoolName, DeadStateData data, EnemyTwo enemy) : base(fsm, entity, animBoolName, data)
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
