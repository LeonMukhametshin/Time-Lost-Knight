using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerWallSlideState : PlayerWallTouchingState
    {
        public PlayerWallSlideState(EntityFSM fsm, CoreSystem core,
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
            movement.SetVelocityY(-UnityEngine.Mathf.Abs(data.wallSlideVelocity));

            if (isExitingState)
            {
                return;
            }

            if (grabInput && yInput == 0)
            {
                fsm.ChangeState<PlayerWallGrabState>();
            }
        }
    }
}

