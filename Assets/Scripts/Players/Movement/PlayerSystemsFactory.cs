using UnityEngine;

namespace Players
{
    public class PlayerSystemsFactory
    {
        public static (
            PlayerInput input,
            GroundCheck groundCheck,
            PlayerJumpSystem jumpSystem,
            PlayerMovementSystem movementSystem
        ) CreateAllSystems(
            PlayerData data,
            Rigidbody2D rigidbody,
            Transform transform,
            Transform groundCheckPosition)
        {
            var inputActions = new InputSystemActions();
            var input = new PlayerInput(inputActions);

            var groundCheck = new GroundCheck(
                groundCheckPosition,
                data.CheckDistance,
                data.GroundLayer
            );

            var jumpSystem = new PlayerJumpSystem(
                rigidbody,
                groundCheck,
                data.JumpForce,
                data.JumpHoldForce,
                data.MaxHoldTime
            );

            var movementSystem = new PlayerMovementSystem(
                data,
                rigidbody,
                transform
            );

            return (input, groundCheck, jumpSystem, movementSystem);
        }
    }
}