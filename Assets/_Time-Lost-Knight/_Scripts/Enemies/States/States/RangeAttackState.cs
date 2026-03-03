using UnityEngine;

public class RangeAttackState : AttackState
{
    protected RangeAttackData data;

    protected GameObject projectile;
    protected IProjectile projectileScript;

    public RangeAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, Transform attackPosition,
        RangeAttackData data) 
        : base(fsm, core, animBoolName, entity, attackPosition)
    {
        this.data = data;
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();

        projectile = GameObject.Instantiate(data.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<IProjectile>();
        projectileScript.Initialize(data);
    }
}