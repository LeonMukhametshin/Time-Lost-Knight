public class EnemyThreePlayerDetectedState : PlayerDetectedState
{
    public EnemyThreePlayerDetectedState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, PlayerDetectedData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }

    public override void Update()
    {
        base.Update();

        if(performeCloseRangeAction)
        {
            fsm.ChangeState<EnemyThreeRangeAttackState>();
        }
    }
}
