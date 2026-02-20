public class EnemyFirstPlayerDetectedState : PlayerDetectedState
{
    private EnemyFirst m_enemy;

    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }
    private FlipContoller m_flipContoller;


    public EnemyFirstPlayerDetectedState(EnemyFSM fsm, Entity entity, 
        string animBoolName, PlayerDetectedData data, EnemyFirst enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (performeCloseRangeAction)
        {
            fsm.SetState(m_enemy.meleeAttackState);
        }
        else if (performeLongRangeAction)
        { 
            fsm.SetState(m_enemy.chargeState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
            fsm.SetState(m_enemy.lookForPlayerState);
        }
        else if(!isDetectingLedge)
        {
            flipController.Flip();
            fsm.SetState(m_enemy.moveState);
        }
    }
}