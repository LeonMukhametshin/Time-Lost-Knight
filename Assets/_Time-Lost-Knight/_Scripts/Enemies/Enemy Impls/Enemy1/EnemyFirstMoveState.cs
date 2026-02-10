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

        if(isPlayerInMinAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
        else if(isDetactingWall || !isDetactingLedge)
        {
            m_enemy.idleState.SetFlipAfterIdle(true);
            fsm.SetState(m_enemy.idleState); 
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}