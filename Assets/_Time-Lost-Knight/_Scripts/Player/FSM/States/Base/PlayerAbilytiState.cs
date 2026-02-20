public class PlayerAbilytiState : PlayerState
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement m_movement;

    protected CollisionDetector collisionDetector
    {
        get => m_collisionDetector ??= core.GetCoreComponent<CollisionDetector>();
    }
    private CollisionDetector m_collisionDetector;

    protected bool isAbilityDone;
    private bool m_isGrounded;

    public PlayerAbilytiState(Player player, EntityFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
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
            fsm.SetState(player.statesContainer.GetState<PlayerIdleState>());
        }
        else
        {
            fsm.SetState(player.statesContainer.GetState<PlayerInAirState>());
        }
    }
}