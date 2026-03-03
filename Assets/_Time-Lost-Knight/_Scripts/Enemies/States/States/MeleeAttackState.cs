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

        var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, 
            data.attackRadius, data.playerMask);
        
        foreach(var obj in detectedObjects)
        {
            if(obj.TryGetComponent<IEffectable>(out var effectable))
            {
                data.effects.ApplyEffect(effectable);
            }
        }
    }
}