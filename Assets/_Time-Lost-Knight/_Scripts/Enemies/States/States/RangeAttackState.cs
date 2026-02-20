using UnityEngine;

public class RangeAttackState : AttackState
{
    protected RangeAttackData data;

    protected GameObject projectile;
    protected Projectile projectileScript;

    public RangeAttackState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, Transform attackPosition,
        RangeAttackData data) 
        : base(fsm, core, animBoolName, entity, attackPosition)
    {
        this.data = data;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = GameObject.Instantiate(data.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Initialize(data);
    }
}