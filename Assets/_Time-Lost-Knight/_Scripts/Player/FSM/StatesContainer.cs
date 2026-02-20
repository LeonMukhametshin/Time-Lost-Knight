using System;
using System.Collections.Generic;

public class StatesContainer
{
    public EntityFSM fsm { get; private set; }
    private Player m_player;
    private PlayerData m_playerData;

    private readonly Dictionary<Type, PlayerState> m_states = new();

    public StatesContainer(Player player, PlayerData playerData)
    {
        m_player = player;
        m_playerData = playerData;

        CreateStates(m_player, m_playerData);
    }

    private void CreateStates(Player player, PlayerData data)
    {
        fsm = new EntityFSM();

        RegisteState(new PlayerIdleState(player, fsm, data, PlayerAnimationConstants.IDLE));
        RegisteState(new PlayerMoveState(player, fsm, data, PlayerAnimationConstants.MOVEMENT));
        RegisteState(new PlayerJumpState(player, fsm, data, PlayerAnimationConstants.IN_AIR));
        RegisteState(new PlayerInAirState(player, fsm, data, PlayerAnimationConstants.IN_AIR));
        RegisteState(new PlayerLandState(player, fsm, data, PlayerAnimationConstants.LAND));
        RegisteState(new PlayerWallSlideState(player, fsm, data, PlayerAnimationConstants.WALL_SLIDE));
        RegisteState(new PlayerWallGrabState(player, fsm, data, PlayerAnimationConstants.WALL_GRAB));
        RegisteState(new PlayerWallClimbState(player, fsm, data, PlayerAnimationConstants.WALL_CLIMB));
        RegisteState(new PlayerWallJumpState(player, fsm, data, PlayerAnimationConstants.IN_AIR));
        RegisteState(new PlayerWallJumpState(player, fsm, data, PlayerAnimationConstants.IN_AIR));
        RegisteState(new PlayerLedgeClibmState(player, fsm, data, PlayerAnimationConstants.LEDGE_CLIMB_STATE));
        RegisteState(new PlayerDashState(player, fsm, data, PlayerAnimationConstants.IN_AIR));
        RegisteState(new PlayerCrouchIdleState(player, fsm, data, PlayerAnimationConstants.CROUCH_IDLE));
        RegisteState(new PlayerCrouchMoveState(player, fsm, data, PlayerAnimationConstants.CROUCH_MOVE));
        RegisteState(new PlayerDropDownState(player, fsm, data, PlayerAnimationConstants.IN_AIR));
        RegisteState(new PlayerPrimaryAttackState(player, fsm, data, PlayerAnimationConstants.ATTACK));
    }

    public void SetBaseState()
    {
        fsm.ChangeState<PlayerIdleState>();
    }

    private void RegisteState<T>(T state) where T : PlayerState
        => m_states[typeof(T)] = state;

    public T GetState<T>() where T : PlayerState 
        => (T)m_states[typeof(T)];
}