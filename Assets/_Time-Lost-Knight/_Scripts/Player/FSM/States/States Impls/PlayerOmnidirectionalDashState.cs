using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerOmnidirectionalDashState : PlayerBaseDashState
    {
        protected override bool canHoldDirection => true;
        protected override bool showDashVisualizer => true;

        public PlayerOmnidirectionalDashState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
        }

        protected override Vector2 ResolveDashDirection(Vector2 fallbackDirection)
        {
            Vector2 inputDirection = player.inputHandler.dashDirectionInput;

            if (inputDirection != Vector2.zero)
            {
                return inputDirection.normalized;
            }

            return fallbackDirection;
        }
    }
}

