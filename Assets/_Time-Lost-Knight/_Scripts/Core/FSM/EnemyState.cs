public class EnemyState : EntityState
{
    protected Entity entity;

    public EnemyState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity) 
        : base(fsm, core, animBoolName)
    {
        this.entity = entity;
    }

    public override void Enter() 
    {
        base.Enter();
        entity.animator.SetBool(animBoolName, true);
    }

    public override void Exit() =>
        entity.animator.SetBool(animBoolName, false);
}