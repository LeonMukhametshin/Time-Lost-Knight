using UnityEngine;

public class RangeAttackState : AttackState
{
    protected RangeAttackData data;

    protected GameObject projectile;
    protected Projectile projectileScript;

    public RangeAttackState(FSM fsm, Entity entity, string animBoolName, Transform attackPosition, RangeAttackData data) 
        : base(fsm, entity, animBoolName, attackPosition)
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

        projectile = GameObject.Instantiate(data.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Initialize(data.projectileSpeed, data.projectileTrevelDistance, data.projectileDamage);
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}