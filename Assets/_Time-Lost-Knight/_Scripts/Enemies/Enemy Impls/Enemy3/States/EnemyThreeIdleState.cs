public class EnemyThreeIdleState : IdleState
{
    public EnemyThreeIdleState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, IdleStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(isPlayerInMinAgroRange)
        {
            fsm.ChangeState<EnemyThreePlayerDetectedState>();
        }
    }
}
