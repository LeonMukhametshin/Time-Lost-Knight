using System;
using UnityEngine;

public class Player : Entity
{
    [field: SerializeField] public AudioSource source { get; private set; }
    [field: SerializeField] public PlayerInventory inventory { get; private set; }
    [field: SerializeField] public DashVizualizer dashVizualizer { get; private set; }

    [field: NonSerialized] public PlayerInputHandler inputHandler { get; private set; }

    public override void Awake()
    {
        base.Awake();

        inputHandler = ServiceLocator.Get<PlayerInputHandler>();

        var playerData = data as PlayerData;

        fsm.Initialize(
            new PlayerIdleState(fsm, core, PlayerAnimationConstants.IDLE, this, playerData),
            new PlayerMoveState(fsm, core, PlayerAnimationConstants.MOVEMENT, this, playerData),
            new PlayerJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData),
            new PlayerAirState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData),
            new PlayerLandState(fsm, core, PlayerAnimationConstants.LAND, this, playerData),
            new PlayerWallSlideState(fsm, core, PlayerAnimationConstants.WALL_SLIDE, this, playerData),
            new PlayerWallGrabState(fsm, core, PlayerAnimationConstants.WALL_GRAB, this, playerData),
            new PlayerWallClimbState(fsm, core, PlayerAnimationConstants.WALL_CLIMB, this, playerData),
            new PlayerWallJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData),
            new PlayerLedgeClibmState(fsm, core, PlayerAnimationConstants.LEDGE_CLIMB_STATE, this, playerData),

            new PlayerForwardDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData),
            new PlayerOmnidirectionalDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData),

            new PlayerCrouchIdleState(fsm, core, PlayerAnimationConstants.CROUCH_IDLE, this, playerData),
            new PlayerCrouchMoveState(fsm, core, PlayerAnimationConstants.CROUCH_MOVE, this, playerData),
            new PlayerDropDownState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData),
            new PlayerPrimaryAttackState(fsm, core, PlayerAnimationConstants.ATTACK, this, playerData),
            new PlayerSecondaryAttackState(fsm, core, PlayerAnimationConstants.ATTACK, this, playerData));

        fsm.GetState<PlayerPrimaryAttackState>().SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        fsm.GetState<PlayerSecondaryAttackState>().SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);

        fsm.ChangeState<PlayerIdleState>();

        //TODO: remove 
        animationToFSM.Initialize(fsm);
    }
}