public class EnemyTwoStunState : StanState
{
    private EnemyTwo m_enemy;

    public EnemyTwoStunState(FSM fsm, Entity entity,
        string animBoolName, StunStateData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if(isStunTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                fsm.SetState(m_enemy.playerDetectedState);
            }
            else
            {
                fsm.SetState(m_enemy.lookForPlayerState);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
