using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData data;

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

        var detectedObject = Physics2D.OverlapCircle(attackPosition.position, 
            data.attackRadius, data.playerMask);

        data.effects.ApplyEffect(ServiceLocator.Get<Player>().core.effectables);
    }
}