using UnityEngine;

public class EnemyTwoRangeAttackState : RangeAttackState
{
    private EnemyTwo m_enemy;

    public EnemyTwoRangeAttackState(FSM fsm, Entity entity, 
        string animBoolName, Transform attackPosition, RangeAttackData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, attackPosition, data)
    {
        m_enemy = enemy;
    }

   
    public override void Update()
    {
        base.Update();

        if(!isAnimationFinished)
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