using UnityEngine;

public class EnemyFirstMeleeAttackState : MeleeAttackState
{
    private EnemyFirst enemy;

    public EnemyFirstMeleeAttackState(FSM fsm, 
        Entity entity, 
        string animBoolName, 
        Transform attackPosition, 
        MeleeAttackStateData data, 
        EnemyFirst enemy) 
        : base(fsm, entity, animBoolName, attackPosition, data)
    {
        this.enemy = enemy;
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
            fsm.SetState(enemy.playerDetectedState);
        }
        else
        {
            fsm.SetState(enemy.lookForPlayerState);
        }
    }
}