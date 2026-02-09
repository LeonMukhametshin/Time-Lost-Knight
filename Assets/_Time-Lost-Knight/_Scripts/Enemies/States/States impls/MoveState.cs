public class MoveState : State
{
    protected MoveStateData data;

    protected bool isDetactingWall;
    protected bool isDetactingLedge;
    protected bool isPlayerInMinAgroRange;

    public MoveState(FSM fsm, Entity entity, string animBoolName, MoveStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        isDetactingLedge = entity.CheckLedge();
        isDetactingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(data.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
