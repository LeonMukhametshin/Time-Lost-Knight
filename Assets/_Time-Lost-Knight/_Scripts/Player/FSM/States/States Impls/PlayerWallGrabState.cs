using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerWallGrabState : PlayerWallTouchingState
    {
        private Vector2 m_holdPosition;

        public PlayerWallGrabState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
        }

        public override void Enter()
        {
            base.Enter();

            m_holdPosition = player.transform.position;
            HoldPosition();
        }

        public override void Update()
        {
            base.Update();

            HoldPosition();

            if (isExitingState)
            {
                return;
            }

            if (yInput > 0)
            {
                fsm.ChangeState<PlayerWallClimbState>();;
            }
            else if (yInput < 0 || !grabInput)
            {
                fsm.ChangeState<PlayerWallSlideState>();;
            }
        }

        private void HoldPosition()
        {
            player.transform.position = m_holdPosition;

            movement.SetVelocityZero();
        }
    }
}

