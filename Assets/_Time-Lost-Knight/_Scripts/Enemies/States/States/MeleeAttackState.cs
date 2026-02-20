using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData data;

    private FlipContoller flipContoller => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    private FlipContoller m_flipContoller;

    public MeleeAttackState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, Transform attackPosition, 
        MeleeAttackStateData data) 
        : base(fsm, core, animBoolName, entity, attackPosition)
    {
        this.data = data;
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();

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