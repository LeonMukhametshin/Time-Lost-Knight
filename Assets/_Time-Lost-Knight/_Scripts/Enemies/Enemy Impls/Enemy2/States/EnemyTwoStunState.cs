public class EnemyTwoStunState : StanState
{
    private EnemyTwo m_enemy;

    public EnemyTwoStunState(EnemyFSM fsm, Entity entity,
        string animBoolName, StunStateData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (!isStunTimeOver)
        {
            return;
        }

        if (isPlayerInMinAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
        else
        {
            fsm.SetState(m_enemy.lookForPlayerState);
        }
    }
}