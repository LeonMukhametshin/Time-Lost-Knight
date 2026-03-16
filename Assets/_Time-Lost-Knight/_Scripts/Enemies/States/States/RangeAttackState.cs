using Game.Core.CoreComponents;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using Game.Projectiles;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States
{
    [MovedFrom("")]
    public class RangeAttackState : AttackState
    {
        protected RangeAttackData data;

        protected GameObject projectile;
        protected IProjectile projectileScript;

        public RangeAttackState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity, Transform attackPosition,
            RangeAttackData data)
            : base(fsm, core, animBoolName, entity, attackPosition)
        {
            this.data = data;
        }

        public override void TriggerAnimation()
        {
            base.TriggerAnimation();

            projectile = GameObject.Instantiate(data.projectile, attackPosition.position, attackPosition.rotation);
            projectileScript = projectile.GetComponent<IProjectile>();

            projectileScript.Initialize(CalculateShotDirection(), data.speed);

            SetLayer(projectile);
        }

        protected virtual Vector2 CalculateShotDirection() =>
            new Vector2(playerPosition.Value.x, attackPosition.position.y);
    }
}
