using UnityEngine;

public class EnemyTwoPlayerDetectedState : PlayerDetectedState
{
    private EnemyTwo m_enemy;

    public EnemyTwoPlayerDetectedState(FSM fsm, Entity entity, 
        string animBoolName, PlayerDetectedData data, EnemyTwo enemy) 
        : base(fsm, entity, animBoolName, data)
    {
        m_enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if(performeCloseRangeAction)
        {
            if(Time.time >= m_enemy.dodgeState.startTime + m_enemy.m_dodgeStateData.dodgeCooldown)
            {
                fsm.SetState(m_enemy.dodgeState);
            }
            else
            {
                fsm.SetState(m_enemy.meleeAttackState);
            }
        }
        else if(performeLongRangeAction)
        {
            fsm.SetState(m_enemy.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            fsm.SetState(m_enemy.playerDetectedState);
        }
    }
}