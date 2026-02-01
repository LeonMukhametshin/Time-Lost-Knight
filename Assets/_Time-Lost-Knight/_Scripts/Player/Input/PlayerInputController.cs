using UnityEngine;

namespace Inputs
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        private GameInput m_gameInput;

        private MovementInputHandler m_movementInput;
        private AttackInputHandler m_attackInput;
        private InteractionInputHandler m_interactInput;

        private bool m_isInitialize;

        private void OnDisable() =>
            DeactivatePlayerInput();

        public void Intialize(
            PlayerMovementController movementContoller,
            InteractionController interactionController,
            PlayerAttackSystem attackSystem,
            CoroutineRunner coroutine)
        {
            if(m_isInitialize)
            {
                return;
            }

            m_gameInput = new GameInput();
            ActivatePlayerInput();

            m_movementInput = new MovementInputHandler(m_gameInput, movementContoller);
            m_attackInput = new AttackInputHandler(m_gameInput, attackSystem, coroutine);
            m_interactInput = new InteractionInputHandler(m_gameInput, interactionController);

        }

        private void Update() =>
            m_movementInput.Update();

        public void ActivatePlayerInput() =>
            m_gameInput.Player.Enable();

        public void DeactivatePlayerInput() =>
           m_gameInput.Player.Disable();

        //TODO UI input 
    }
}
