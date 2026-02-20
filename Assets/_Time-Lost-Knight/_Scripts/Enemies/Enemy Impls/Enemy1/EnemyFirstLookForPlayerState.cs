public class EnemyFirstLookForPlayerState : LookForPlayerState
{
    private EnemyFirst m_enemy;

    public EnemyFirstLookForPlayerState(EnemyFSM fsm, Entity entity, 
        string animBoolName, LookForPlayerStateData data, EnemyFirst enemyFirst) 
        : base(fsm, entity, animBoolName, data)
    {
        this.m_enemy = enemyFirst;
    }

    public override void Update()
    {
        base.Update();

        if(isPlayerInMinAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
        else if(isAllTurnsTimeDone)
        {
            fsm.SetState(m_enemy.moveState);
        }
    }
}
