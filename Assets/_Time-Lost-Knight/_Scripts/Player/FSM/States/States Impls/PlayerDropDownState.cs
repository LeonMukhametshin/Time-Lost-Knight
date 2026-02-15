public class PlayerDropDownState : PlayerState
{
    private bool m_isGrounded;

    public PlayerDropDownState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.oneWayPlatformCollisionController.SetIgnorePlatform();
        player.movement.SetVelocityY(-data.dropVelocity);
    }

    public override void DoCheck()
    {
        base.DoCheck();
        m_isGrounded = player.collisionDetector.CheckGrounded();
    }

    public override void Update()
    {
        base.Update();

        if (!m_isGrounded)
        {
            var inAirState = player.statesContainer.GetState<PlayerInAirState>();
            inAirState.StartCoyoteTime();
            fsm.SetState(inAirState);
        }
    }
}