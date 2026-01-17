using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private PlayerMovementController m_movementController;

    //TODO move to entry point
    private void Awake()
    {
        InitializeSystems();
    }

    private void InitializeSystems()
    { 
        m_movementController.Initialize(m_playerData.playerMovement);
    }
}