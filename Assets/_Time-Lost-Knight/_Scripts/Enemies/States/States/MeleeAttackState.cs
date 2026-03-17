using Game.Buffs.Interfaces;
using Game.Core.CoreComponents;
using Game.Core.ServiceLocatorSpace;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States
{
    [MovedFrom("")]
    public class MeleeAttackState : AttackState
    {
        protected MeleeAttackStateData data;

        public MeleeAttackState(EntityFSM fsm, CoreSystem core, string animBoolName,
            Entity entity, Transform attackPosition,
            MeleeAttackStateData data)
            : base(fsm, core, animBoolName, entity, attackPosition)
        {
            this.data = data;
        }

        public override void TriggerAnimation()
        {
            base.TriggerAnimation();

            var detectedObject = Physics2D.OverlapCircle(attackPosition.position,
                data.attackRadius, data.playerMask);

            data.effects.ApplyEffect(ServiceLocator
                .Get<IPlayerFactory>()
                .Create()
                .core.effectables);
        }
    }
}