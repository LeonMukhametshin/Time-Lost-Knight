using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] private InteractionDetector m_interactionDetector;

    private GameInput m_gameInput;

    private void Awake()
    {
        m_gameInput = new GameInput();
        m_gameInput.Player.Enable();
    }

    private void Update()
    {
        if (m_gameInput.Player.Interact.WasPerformedThisFrame())
        {
            m_interactionDetector.OnInteract();
        }
    }
}
