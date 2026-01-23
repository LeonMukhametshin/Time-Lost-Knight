using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InteractInput : MonoBehaviour
{
    [SerializeField] private InteractionController m_interactionController;

    private GameInput m_gameInput;

    private void Awake()
    {
        m_gameInput = new GameInput();
        m_gameInput.Player.Enable();

        m_gameInput.Player.Interact.performed += OnInteract;
    }

    private void OnDestroy()
    {
        m_gameInput.Player.Interact.performed -= OnInteract;
        m_gameInput.Player.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        m_interactionController.Interact();
    }
}
