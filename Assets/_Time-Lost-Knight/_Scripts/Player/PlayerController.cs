using Game.Core.CoreComponents;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.Components;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Impls;
using Game.Player.Input;
using Game.Weapons;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player
{
    [MovedFrom("")]
    public class PlayerController : Entity
    {
        [field: SerializeField] public DashVizualizer dashVizualizer { get; private set; }
        [field: NonSerialized] public PlayerInputHandler inputHandler { get; private set; }

        [SerializeField] private RangeAttackData m_rangeAttackData;
        [SerializeField] private Transform m_rangeAttackPosition;
        [SerializeField] private RangedWeapon m_rangedWeapon;
        [SerializeField] private Weapon m_meleeWeapon;

        private bool m_isInitialized;

        public void Initialize(PlayerInputHandler input)
        {
            if(m_isInitialized)
            {
                return;
            }

            fsm = new PlayerFSM();

            inputHandler = input;

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
                new PlayerRangedAttackState(fsm, core, PlayerAnimationConstants.RANGED_ATTACK, this, playerData, m_rangeAttackPosition, m_rangeAttackData, true),

                new PlayerWallSlideState(fsm, core, PlayerAnimationConstants.WALL_SLIDE, this, playerData, false),
                new PlayerWallClimbState(fsm, core, PlayerAnimationConstants.WALL_CLIMB, this, playerData, false),
                new PlayerWallGrabState(fsm, core, PlayerAnimationConstants.WALL_GRAB, this, playerData, false),
                new PlayerWallJumpState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, false),

                new PlayerForwardDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, false),

                new PlayerOmnidirectionalDashState(fsm, core, PlayerAnimationConstants.IN_AIR, this, playerData, false));

            fsm
                .GetState<PlayerPrimaryAttackState>()
                .SetWeapon(m_meleeWeapon);
            fsm
                .GetState<PlayerRangedAttackState>()
                .SetWeapon(m_rangedWeapon);

            fsm.ChangeState<PlayerIdleState>();

            animationToFSM
                .Initialize(fsm);

            core
                .GetComponent<HealthComponent>()
                .Initialize(data.maxHealth);

            m_isInitialized = true;
        }
    }
}

