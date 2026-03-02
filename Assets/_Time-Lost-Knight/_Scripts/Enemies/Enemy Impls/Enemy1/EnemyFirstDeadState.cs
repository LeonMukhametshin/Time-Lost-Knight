public class EnemyFirstDeadState : DeadState
{
    public EnemyFirstDeadState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        DeadStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }
}