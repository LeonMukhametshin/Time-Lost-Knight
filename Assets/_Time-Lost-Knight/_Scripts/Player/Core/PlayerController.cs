using UnityEngine;

public class PlayerController : MonoBehaviour, IPhysics
{
    [SerializeField] private PlayerMovementController m_movementController;
    [SerializeField] private AttackInputHandler m_attackInput;
    [SerializeField] private HealthSystem m_healthSystem;

    [SerializeField] private Rigidbody2D m_rigidbody;

    private CoroutineRunner m_coroutines;

    private bool isInitialize = false;

    private void Update()
    {
        m_attackInput.Update();
    }

    public void Initialize(PlayerData data, CoroutineRunner coroutine)
    {
        if(isInitialize)
        {
            return;
        }

        m_coroutines = coroutine;

        m_movementController.Initialize(data.playerMovement, m_coroutines);
        m_attackInput.Initialize(m_coroutines);
        m_healthSystem.Initialize(data.healthPoints);

        isInitialize = true;
    }

    public void AddForce(Vector2 direction, ForceMode2D mode) =>
         m_rigidbody.AddForce(direction, mode);
}