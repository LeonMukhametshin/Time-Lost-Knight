using System;
using System.Collections.Generic;

public class StatesContainer
{
    public PlayerFSM fsm { get; private set; }
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
        fsm = new PlayerFSM();

        RegisteState(new PlayerIdleState(player, fsm, data, PlayerAnimationConst.IDLE));
        RegisteState(new PlayerMoveState(player, fsm, data, PlayerAnimationConst.MOVEMENT));
        RegisteState(new PlayerJumpState(player, fsm, data, PlayerAnimationConst.IN_AIR));
        RegisteState(new PlayerInAirState(player, fsm, data, PlayerAnimationConst.IN_AIR));
        RegisteState(new PlayerLandState(player, fsm, data, PlayerAnimationConst.LAND));
        RegisteState(new PlayerWallSlideState(player, fsm, data, PlayerAnimationConst.WALL_SLIDE));
        RegisteState(new PlayerWallGrabState(player, fsm, data, PlayerAnimationConst.WALL_GRAB));
        RegisteState(new PlayerWallClimbState(player, fsm, data, PlayerAnimationConst.WALL_CLIMB));
        RegisteState(new PlayerWallJumpState(player, fsm, data, PlayerAnimationConst.IN_AIR));
        RegisteState(new PlayerWallJumpState(player, fsm, data, PlayerAnimationConst.IN_AIR));
        RegisteState(new PlayerLedgeClibmState(player, fsm, data, PlayerAnimationConst.LEDGE_CLIMB_STATE));
        RegisteState(new PlayerDashState(player, fsm, data, PlayerAnimationConst.IN_AIR));
        RegisteState(new PlayerCrouchIdleState(player, fsm, data, PlayerAnimationConst.CROUCH_IDLE));
        RegisteState(new PlayerCrouchMoveState(player, fsm, data, PlayerAnimationConst.CROUCH_MOVE));
        RegisteState(new PlayerDropDownState(player, fsm, data, PlayerAnimationConst.IN_AIR));
    }

    public void SetBaseState()
    { 
        if(fsm.currentState is not null)
        {
            return;
        }

        fsm.Initialize(GetState<PlayerIdleState>());
    }

    private void RegisteState<T>(T state) where T : PlayerState
        => m_states[typeof(T)] = state;

    public T GetState<T>() where T : PlayerState 
        => (T)m_states[typeof(T)];
}