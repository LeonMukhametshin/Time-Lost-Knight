using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using Game.Projectiles;
using Game.Weapons;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerRangedAttackState : PlayerAbilytiState, IAnimationTrigger
    {
        private readonly Transform m_attackPosition;
        private readonly RangeAttackData m_data;
        private RangedWeapon m_weapon;

        private const float FallbackExitTime = 0.5f;

        private float m_enterTime;
        private int m_xInput;

        public PlayerRangedAttackState(EntityFSM fsm, CoreSystem core,
           string animBoolName, PlayerController player,
           PlayerData data, Transform attackPosition,
           RangeAttackData rangedData, bool active)
           : base(fsm, core,
               animBoolName, player,
               data, active)
        {
            m_attackPosition = attackPosition;
            m_data = rangedData;
        }

        public override void Enter()
        {
            base.Enter();

            m_enterTime = Time.time;
            player.inputHandler.UseRangedAttackInput();
            m_weapon?.EnterWeapon();
        }

        public override void Exit()
        {
            base.Exit();

            m_weapon?.ExitWeapon();
        }

        public override void Update()
        {
            base.Update();

            m_xInput = player.inputHandler.normalizedInputX;

            if (!isExitingState)
            {
                flipController.CheckIfShoudFlip(m_xInput);
                movement.SetVelocityXSmooth(data.movementSpeed * m_xInput, data.movementAcceleration, data.movementDeceleration);
            }

            if (m_weapon == null && !isAbilityDone && Time.time - m_enterTime >= FallbackExitTime)
            {
                isAbilityDone = true;
            }
        }

        public void SetWeapon(RangedWeapon weapon)
        {
            m_weapon = weapon;
            m_weapon?.Initialize(this);
        }

        public override void TriggerAnimation()
        {
            if (m_data == null || m_attackPosition == null)
                return;

            var projectileInstance = Object.Instantiate(m_data.projectile, m_attackPosition.position, m_attackPosition.rotation);
            if (projectileInstance.TryGetComponent(out IProjectile projectile))
            {
                Vector2 direction = Vector2.right * flipController.facingDirection;
         
                Vector2 targetPoint = (Vector2)m_attackPosition.position + direction * 100f; 

                projectile.Initialize(targetPoint, m_data.speed);
            }
        }

        public override void FinishAnimation()
        {
            isAbilityDone = true;
        }

        protected void SetLayer(GameObject visualEffect) =>
            visualEffect.layer = player.gameObject.layer;
    }
}

