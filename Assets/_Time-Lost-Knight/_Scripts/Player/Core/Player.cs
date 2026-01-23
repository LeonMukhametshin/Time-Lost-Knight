using UnityEngine;

public class Player : MonoBehaviour, IPhysics
{
    [SerializeField] private Rigidbody2D m_rigidbody;

    [SerializeField] private PlayerData m_data;
    [SerializeField] private PlayerMovementController m_movementController;
    [SerializeField] private AttackInputHandler m_attackInput;
    [SerializeField] private HealthSystem m_healthSystem;

    //TODO move to entry point
    private void Awake()
    {
        InitializeSystems();
    }

    private void Update()
    {
        m_attackInput.Update();
    }

    private void InitializeSystems()
    { 
        m_movementController.Initialize(m_data.playerMovement);
        m_attackInput.Initialize();
        m_healthSystem.Initialize(m_data.healthPoints);

        m_healthSystem.valueChanged += () => Debug.Log(m_healthSystem.value);
    }

    public void AddForce(Vector2 direction, ForceMode2D mode) =>
         m_rigidbody.AddForce(direction, mode);
}