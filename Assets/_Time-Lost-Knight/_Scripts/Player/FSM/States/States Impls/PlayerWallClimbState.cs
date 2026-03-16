using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerWallClimbState : PlayerWallTouchingState
    {
        public PlayerWallClimbState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
        }

        public override void Update()
        {
            base.Update();

            movement.SetVelocityY(data.wallClimbVelocity);

            if (isExitingState)
            {
                return;
            }

            if (yInput != 1)
            {
                fsm.ChangeState<PlayerWallGrabState>();
            }
        }
    }
}

