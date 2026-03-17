using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(EntityFSM fsm, CoreSystem core,
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
            movement.SetVelocityXSmooth(0f, data.movementAcceleration, data.movementDeceleration);
        }

        public override void Update()
        {
            base.Update();

            if (isExitingState)
            {
                return;
            }

            movement.SetVelocityXSmooth(0f, data.movementAcceleration, data.movementDeceleration);

            if (xInput != 0)
            {
                fsm.ChangeState<PlayerMoveState>();
            }
            else if (yInput == -1)
            {
                fsm.ChangeState<PlayerCrouchIdleState>();
            }
        }
    }
}

