public class EnemyTwoLookForPlayerState : LookForPlayerState
{
    private EnemyTwo m_enemy;

    public EnemyTwoLookForPlayerState(FSM fsm, Entity entity, 
        string animBoolName, LookForPlayerStateData data, EnemyTwo enemy) 
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
        else if(isAllTurnsTimeDone)
        {
            fsm.SetState(m_enemy.moveState);
        }
    }
}