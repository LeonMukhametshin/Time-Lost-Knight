public class MoveState : State
{
    protected MoveStateData data;

    protected bool isDetactingWall;
    protected bool isDetactingLedge;
    protected bool isPlayerInMinAgroRange;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    private EnemyCollisionDetector enemyCollisionDetector
    {
        get => m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    }

    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public MoveState(FSM fsm, Entity entity, 
        string animBoolName, MoveStateData data) 
        : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        isDetactingLedge = enemyCollisionDetector.CheckLedge();
        isDetactingWall = enemyCollisionDetector.CheckWallTouch();
        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
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
