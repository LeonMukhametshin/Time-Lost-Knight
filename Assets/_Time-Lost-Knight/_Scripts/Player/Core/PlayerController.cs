using Inputs;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovementController m_movementController;

    [SerializeField] private InteractionController m_interactionController;
    [SerializeField] private PlayerAttackSystem m_attackSystem;

    [SerializeField] private HealthSystem m_healthSystem;

    [SerializeField] private PlayerInputController m_inputController;

    private CoroutineRunner m_coroutines;

    private bool isInitialize = false;

    public void Initialize(PlayerData data, CoroutineRunner coroutine)
    {
        if(isInitialize)
        {
            return;
        }

        m_coroutines = coroutine;

        m_inputController.Intialize();

        m_healthSystem.Initialize(data.healthPoints);
        m_movementController.Initialize(data.playerMovement, m_coroutines);

        isInitialize = true;
    }
}