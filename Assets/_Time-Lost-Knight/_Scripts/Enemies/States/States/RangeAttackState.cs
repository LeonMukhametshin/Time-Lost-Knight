using UnityEngine;

public class RangeAttackState : AttackState
{
    protected RangeAttackData data;
   
    private FlipContoller flipController =>
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    private FlipContoller m_flipContoller;

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

        projectileScript.Initialize(CalculateShotDirection(), data.speed);

        SetLayer(projectile);
    }

    protected virtual Vector2 CalculateShotDirection() =>
        new Vector2(playerPosition.Value.x, attackPosition.position.y);
}