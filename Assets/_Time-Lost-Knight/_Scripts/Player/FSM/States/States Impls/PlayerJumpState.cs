using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerJumpState : PlayerAbilytiState
    {
        private int amountOfJumpsLeft;

        public PlayerJumpState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
            amountOfJumpsLeft = data.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();

            player.inputHandler.UseJumpInput();
            movement.SetVelocityY(data.jumpVelocity);
            isAbilityDone = true;

            DecreaseAmountOfJumpLeft();
            fsm.GetState<PlayerAirState>().SetIsJumping();
        }

        public bool CanJump() =>
            amountOfJumpsLeft > 0;

        public void ResetAmountOfJumpsLeft() =>
            amountOfJumpsLeft = data.amountOfJumps;

        public void DecreaseAmountOfJumpLeft() =>
            amountOfJumpsLeft--;
    }
}

