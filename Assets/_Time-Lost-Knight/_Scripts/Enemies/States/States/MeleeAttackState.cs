using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData data;

    protected AttackDetails attackDetails;

    public MeleeAttackState(FSM fsm, Entity entity, string animBoolName, Transform attackPosition, MeleeAttackStateData data) : base(fsm, entity, animBoolName, attackPosition)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmout = data.attackDamage;
        attackDetails.position = entity.aliveGameObject.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, data.attackRadius, data.playerMask);
        
        foreach(var obj in detectedObjects)
        {
            if(obj.TryGetComponent<IDamageable>(out var damageable))
            {
                //damageable.TakeDamage(attackDetails);
            }
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}