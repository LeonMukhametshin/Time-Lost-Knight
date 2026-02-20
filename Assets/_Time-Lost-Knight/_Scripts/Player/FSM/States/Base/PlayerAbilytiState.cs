public class PlayerAbilytiState : PlayerState
{
    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    protected FlipContoller flipController =>
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    protected CollisionDetector collisionDetector => 
        m_collisionDetector ??= core.GetCoreComponent<CollisionDetector>();

    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private CollisionDetector m_collisionDetector;

    protected bool isAbilityDone;
    private bool m_isGrounded;

    public PlayerAbilytiState(EntityFSM fsm, Core core,
        string animBoolName, Player player,
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        m_isGrounded = collisionDetector.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Update()
    {
        base.Update();

        if (!isAbilityDone)
        {
            return;
        }

        if (m_isGrounded && movement.currentVelocity.y < 0.1f)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
        else
        {
            fsm.ChangeState<PlayerAirState>();
        }
    }
}