using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private PlayerMovementController m_movementController;
    [SerializeField] private AttackInputHandler m_attackInput;

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
        m_movementController.Initialize(m_playerData.playerMovement);
        m_attackInput.Initialize();
    }
}