public class EnemyFirstDeadState : DeadState
{
    private EnemyFirst m_enemy;

    public EnemyFirstDeadState(FSM fsm, Entity entity, 
        string animBoolName, DeadStateData data, EnemyFirst enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }
}