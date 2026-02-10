public class EnemyFirstPlayerDetectedState : PlayerDetectedState
{
    private EnemyFirst m_enemy;

    public EnemyFirstPlayerDetectedState(FSM fsm, Entity entity, string animBoolName, PlayerDetectedData data, EnemyFirst enemy) : base(fsm, entity, animBoolName, data)
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
            entity.Flip();
            fsm.SetState(m_enemy.moveState);
        }
    }
}