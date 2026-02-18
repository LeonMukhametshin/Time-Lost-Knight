public class MoveState : State
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    private Movement m_movement;
    private FlipContoller m_flipContoller;

    protected MoveStateData data;

    protected bool isDetactingWall;
    protected bool isDetactingLedge;
    protected bool isPlayerInMinAgroRange;

    public MoveState(FSM fsm, Entity entity, 
        string animBoolName, MoveStateData data) 
        : base(fsm, entity, animBoolName)
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
        movement.SetVelocityX(data.movementSpeed);
    }

    public override void Update()
    {
        base.Update();

        movement.SetVelocityX(data.movementSpeed * flipController.facingDirection);
    }
}
