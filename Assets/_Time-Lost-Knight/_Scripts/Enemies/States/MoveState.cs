public class MoveState : State
{
    protected MoveStateData data;

    protected bool isDetactingWall;
    protected bool isDetactingLedge;

    public MoveState(FSM fsm, Entity entity, string animBoolName, MoveStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(data.movementSpeed);

        isDetactingLedge = entity.CheckLedge();
        isDetactingWall = entity.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        isDetactingLedge = entity.CheckLedge();
        isDetactingWall = entity.CheckWall();
    }

    public override void Update()
    {
        base.Update();
    }
}
