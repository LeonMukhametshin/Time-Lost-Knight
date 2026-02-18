using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData data;

    protected FlipContoller flipContoller
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }
    private FlipContoller m_flipContoller;

    public MeleeAttackState(FSM fsm, Entity entity, 
        string animBoolName, Transform attackPosition, MeleeAttackStateData data) 
        : base(fsm, entity, animBoolName, attackPosition)
    {
        this.data = data;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, 
            data.attackRadius, data.playerMask);
        
        foreach(var obj in detectedObjects)
        {
            if(obj.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(data.attackDamage);
            }
            if(obj.TryGetComponent<IKnockbackable>(out var knockbackable))
            {
                knockbackable.Knockback(data.angle, data.knokbackStringth, flipContoller.facingDirection);
            }
        }
    }
}