using System;
using UnityEngine;

public class Player : Entity
{
    [field: SerializeField] public PlayerInventory inventory { get; private set; }
    [field: SerializeField] public DashVizualizer dashVizualizer { get; private set; }

    [field: NonSerialized] public PlayerInputHandler inputHandler { get; private set; }

    public override void Awake()
    {
        fsm = new PlayerFSM();

        inputHandler = ServiceLocator.Get<PlayerInputHandler>();

        var playerData = data as PlayerData;

        fsm.Initialize(
            new PlayerIdleState(fsm, core, PlayerAnimationConstants.IDLE, this, playerData, true),
            new PlayerMoveState(fsm, core, PlayerAnimationConstants.MOVEMENT, this, playerData, true),
            new PlayerJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, true),
            new PlayerAirState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, true),
            new PlayerLandState(fsm, core, PlayerAnimationConstants.LAND, this, playerData, true),
            new PlayerLedgeClibmState(fsm, core, PlayerAnimationConstants.LEDGE_CLIMB_STATE, this, playerData, true),
            new PlayerCrouchIdleState(fsm, core, PlayerAnimationConstants.CROUCH_IDLE, this, playerData, true),
            new PlayerCrouchMoveState(fsm, core, PlayerAnimationConstants.CROUCH_MOVE, this, playerData, true),
            new PlayerDropDownState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, true),
            
            new PlayerPrimaryAttackState(fsm, core, PlayerAnimationConstants.ATTACK, this, playerData, true),
            new PlayerSecondaryAttackState(fsm, core, PlayerAnimationConstants.ATTACK, this, playerData, true),

            new PlayerWallSlideState(fsm, core, PlayerAnimationConstants.WALL_SLIDE, this, playerData, false),
            new PlayerWallClimbState(fsm, core, PlayerAnimationConstants.WALL_CLIMB, this, playerData, false),
            new PlayerWallGrabState(fsm, core, PlayerAnimationConstants.WALL_GRAB, this, playerData, false),
            new PlayerWallJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, false),

            new PlayerForwardDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, false),
            new PlayerOmnidirectionalDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, false));

        fsm
            .GetState<PlayerPrimaryAttackState>()
            .SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        fsm
            .GetState<PlayerSecondaryAttackState>()
            .SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);

        fsm.ChangeState<PlayerIdleState>();

        //TODO: remove 
        animationToFSM.Initialize(fsm);

        core
            .GetComponent<HealthComponent>()
            .Initialize(data.maxHealth);
    }
}