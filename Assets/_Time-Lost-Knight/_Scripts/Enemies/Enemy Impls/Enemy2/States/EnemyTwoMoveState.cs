public class EnemyTwoMoveState : MoveState
{
    private EnemyTwo m_enemy;

    public EnemyTwoMoveState(FSM fsm, Entity entity, 
        string animBoolName, MoveStateData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (isPlayerInMinAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
        else if (isDetactingWall || !isDetactingLedge)
        {
            m_enemy.idleState.SetFlipAfterIdle(true);
            fsm.SetState(m_enemy.idleState);
        }
    }
}