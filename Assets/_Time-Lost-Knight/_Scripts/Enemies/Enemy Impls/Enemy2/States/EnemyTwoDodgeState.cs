public class EnemyTwoDodgeState : DodgeState
{
    private EnemyTwo m_enemy;

    public EnemyTwoDodgeState(FSM fsm, Entity entity, string animBoolName, DodgeStateData data, EnemyTwo enemy) : base(fsm, entity, animBoolName, data)
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

        if(isDodgeOver)
        {
            if(isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                fsm.SetState(m_enemy.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                fsm.SetState(m_enemy.rangeAttackState);
            }
            else if(!isPlayerInMaxAgroRange) 
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