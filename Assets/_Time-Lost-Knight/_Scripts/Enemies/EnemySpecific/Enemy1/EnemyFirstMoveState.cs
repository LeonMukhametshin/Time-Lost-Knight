public class EnemyFirstMoveState : MoveState
{
    private EnemyFirst m_enemy;

    public EnemyFirstMoveState(FSM fsm, 
        Entity entity, 
        string animBoolName, 
        MoveStateData data, 
        EnemyFirst enemy) 
        : base(fsm, entity, animBoolName, data)
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


    public override void Update()
    {
        base.Update();

        if(isDetactingWall || !isDetactingLedge)
        {
            fsm.SetState(m_enemy.idleState); 
            m_enemy.idleState.SetFlipAfredIdle(true);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}