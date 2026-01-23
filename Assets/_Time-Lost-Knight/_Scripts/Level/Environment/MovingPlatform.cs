using UnityEngine;

public class MovingPlatform : Subject, IInteractable
{
    [SerializeField] private PlanformMover m_movePlatform;

    [SerializeField] private int m_priority = 3;
    [SerializeField] private Transform m_intarectionPoint;

    public int priority => m_priority;
    public Transform interactionPoint => m_intarectionPoint;

    private void Awake()
    {
        m_movePlatform.Initialize();
    }

    public void Interact()
    {
        if (CanInteract())
        {
            m_movePlatform.Activate();
        }
    }

    public bool CanInteract() =>
        m_movePlatform.CanMove();
}