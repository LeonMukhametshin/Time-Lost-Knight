public class MoveState : EnemyState
{
    protected MoveStateData data;

    protected bool isDetactingWall;
    protected bool isDetactingLedge;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    protected FlipContoller flipController => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    
    private EnemyCollisionDetector enemyCollisionDetector => 
        m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public MoveState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        MoveStateData data) 
        : base(fsm, core, animBoolName, entity)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        isDetactingLedge = enemyCollisionDetector.CheckLedge();
        isDetactingWall = enemyCollisionDetector.CheckWallTouch();
        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMaxAgroRange();
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
