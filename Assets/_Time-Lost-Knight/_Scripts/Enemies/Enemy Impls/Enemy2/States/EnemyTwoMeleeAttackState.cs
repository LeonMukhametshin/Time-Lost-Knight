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

    public override void Update()
    {
        base.Update();

        if (!isAnimationFinished)
        {
            return;
        }

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