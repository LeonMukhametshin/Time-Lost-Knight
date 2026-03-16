using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerForwardDashState : PlayerBaseDashState
    {
        protected override bool canHoldDirection => false;
        protected override bool showDashVisualizer => false;

        private Vector2 m_workspace;

        public PlayerForwardDashState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool canUse)
            : base(fsm, core, animBoolName, player, data, canUse)
        {
        }

        protected override Vector2 ResolveDashDirection(Vector2 fallbackDirection)
        {
            m_workspace.Set(fallbackDirection.x, 0f);
            return m_workspace.normalized;
        }
    }
}

