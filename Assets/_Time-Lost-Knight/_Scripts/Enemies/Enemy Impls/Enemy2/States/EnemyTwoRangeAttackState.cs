using UnityEngine;

public class EnemyTwoRangeAttackState : RangeAttackState
{
    private EnemyTwo m_enemy;

    public EnemyTwoRangeAttackState(FSM fsm, Entity entity, string animBoolName, Transform attackPosition, RangeAttackData data, EnemyTwo enemy) : base(fsm, entity, animBoolName, attackPosition, data)
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

        if(isAnimationFinished)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
