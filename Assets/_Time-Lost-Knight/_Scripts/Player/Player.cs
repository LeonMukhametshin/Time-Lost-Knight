using System;
using TMPro;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private PlayerData m_data;

    [field: SerializeField] public PlayerInventory inventory { get; private set; }
    [field: SerializeField] public DashVizualizer dashVizualizer { get; private set; }
    [field: NonSerialized] public PlayerInputHandler inputHandler { get; private set; }

    public override void Awake()
    {
        base.Awake();

        inputHandler = ServiceLocator.Get<PlayerInputHandler>();

        fsm.Initialize(
            new PlayerIdleState(fsm, core, PlayerAnimationConstants.IDLE, this, m_data),
            new PlayerMoveState(fsm, core, PlayerAnimationConstants.MOVEMENT, this, m_data),
            new PlayerJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, m_data),
            new PlayerAirState(fsm, core, PlayerAnimationConstants.IN_AIR, this, m_data),
            new PlayerLandState(fsm, core, PlayerAnimationConstants.LAND, this, m_data),
            new PlayerWallSlideState(fsm, core, PlayerAnimationConstants.WALL_SLIDE, this, m_data),
            new PlayerWallGrabState(fsm, core, PlayerAnimationConstants.WALL_GRAB, this, m_data),
            new PlayerWallClimbState(fsm, core, PlayerAnimationConstants.WALL_CLIMB, this, m_data),
            new PlayerWallJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, m_data),
            new PlayerLedgeClibmState(fsm, core, PlayerAnimationConstants.LEDGE_CLIMB_STATE, this, m_data),

            new PlayerForwardDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, m_data),
            new PlayerOmnidirectionalDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, m_data),

            new PlayerCrouchIdleState(fsm, core, PlayerAnimationConstants.CROUCH_IDLE, this, m_data),
            new PlayerCrouchMoveState(fsm, core, PlayerAnimationConstants.CROUCH_MOVE, this, m_data),
            new PlayerDropDownState(fsm, core, PlayerAnimationConstants.IN_AIR, this, m_data),
            new PlayerPrimaryAttackState(fsm, core, PlayerAnimationConstants.ATTACK, this, m_data),
            new PlayerSecondaryAttackState(fsm, core, PlayerAnimationConstants.ATTACK, this, m_data));

        fsm.GetState<PlayerPrimaryAttackState>().SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        fsm.GetState<PlayerSecondaryAttackState>().SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);

        fsm.ChangeState<PlayerIdleState>();

        //TODO: remove 
        animationToFSM.Initialize(fsm);
    }

    public override void Update()
    {
        base.Update();

        _textMeshPro.text = fsm.currentState.ToString();
    }
}