public class StatesContainer
{
    public PlayerFSM fsm { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState airState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLedgeClibmState playerLedgeClibmState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMoveState crouchMoveState { get; private set; }

    private Player m_player;
    private PlayerData m_playerData;

    public bool initialized { get; private set; }

    public StatesContainer(Player player, PlayerData playerData)
    {
        m_player = player;
        m_playerData = playerData;

        fsm = new PlayerFSM();

        CreateStates(m_player, m_playerData);
    }

    private void CreateStates(Player player, PlayerData data)
    {
        //TODO: factory 
        idleState = new PlayerIdleState(player, fsm, data, PlayerAnimationConst.IDLE);
        moveState = new PlayerMoveState(player, fsm, data, PlayerAnimationConst.MOVEMENT);
        jumpState = new PlayerJumpState(player, fsm, data, PlayerAnimationConst.IN_AIR);
        airState = new PlayerInAirState(player, fsm, data, PlayerAnimationConst.IN_AIR);
        landState = new PlayerLandState(player, fsm, data, PlayerAnimationConst.LAND);
        wallSlideState = new PlayerWallSlideState(player, fsm, data, PlayerAnimationConst.WALL_SLIDE);
        wallGrabState = new PlayerWallGrabState(player, fsm, data, PlayerAnimationConst.WALL_GRAB);
        wallClimbState = new PlayerWallClimbState(player, fsm, data, PlayerAnimationConst.WALL_CLIMB);
        wallJumpState = new PlayerWallJumpState(player, fsm, data, PlayerAnimationConst.IN_AIR);
        playerLedgeClibmState = new PlayerLedgeClibmState(player, fsm, data, PlayerAnimationConst.LEDGE_CLIMB_STATE);
        dashState = new PlayerDashState(player, fsm, data, PlayerAnimationConst.IN_AIR);
        crouchIdleState = new PlayerCrouchIdleState(player, fsm, data, PlayerAnimationConst.CROUCH_IDLE);
        crouchMoveState = new PlayerCrouchMoveState(player, fsm, data, PlayerAnimationConst.CROUCH_MOVE);
    }

    public void Initalize()
    {
        if(initialized)
        {
            return;
        }

        fsm.Initialize(idleState);
        initialized = true;
    }
}