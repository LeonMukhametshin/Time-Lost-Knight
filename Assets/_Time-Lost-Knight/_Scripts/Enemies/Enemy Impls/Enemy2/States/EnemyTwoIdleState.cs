public class EnemyTwoIdleState : IdleState
{
    public EnemyTwo m_enemy;

    public EnemyTwoIdleState(FSM fsm, Entity entity, 
        string animBoolName, IdleStateData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if(isPlayerInMinAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            fsm.SetState(m_enemy.moveState);
        }
    }
}