public class EnemyTwoDeadState : DeadState
{
    public EnemyTwoDeadState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        DeadStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }
}