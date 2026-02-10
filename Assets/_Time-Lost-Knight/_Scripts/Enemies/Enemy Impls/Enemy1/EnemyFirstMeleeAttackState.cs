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
                fsm.SetState(enemy.playerDetectedState);
            }
            else
            {
                fsm.SetState(enemy.lookForPlayerState);
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
