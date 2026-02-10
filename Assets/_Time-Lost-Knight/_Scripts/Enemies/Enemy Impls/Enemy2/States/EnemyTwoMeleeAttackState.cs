using UnityEngine;

public class EnemyTwoMeleeAttackState : MeleeAttackState
{
    private EnemyTwo m_enemy;

    public EnemyTwoMeleeAttackState(FSM fsm, Entity entity, string animBoolName, 
        Transform attackPosition, MeleeAttackStateData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, attackPosition, data)
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

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                fsm.SetState(m_enemy.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange) 
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
