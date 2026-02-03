using UnityEngine;

public class PlayerController : MonoBehaviour, IPhysics
{
    [SerializeField] private PlayerMovementController m_movementController;

    [SerializeField] private InteractionController m_interactionController;
    [SerializeField] private PlayerAttackSystem m_attackSystem;

    [SerializeField] private HealthSystem m_healthSystem;

    [SerializeField] private Rigidbody2D m_rigidbody;

    private CoroutineRunner m_coroutines;

    private bool isInitialize = false;

    public void Initialize(PlayerData data, CoroutineRunner coroutine)
    {
        if(isInitialize)
        {
            return;
        }

        m_coroutines = coroutine;

        m_healthSystem.Initialize(data.healthPoints);
        m_movementController.Initialize(data.playerMovement, m_coroutines, m_rigidbody);

        isInitialize = true;
    }

    public void AddForce(Vector2 direction, ForceMode2D mode) =>
         m_rigidbody.AddForce(direction, mode);
}