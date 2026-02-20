public class EnemyTwoDeadState : DeadState
{
    private EnemyTwo m_enemy;

    public EnemyTwoDeadState(EnemyFSM fsm, Entity entity, 
        string animBoolName, DeadStateData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }
}
