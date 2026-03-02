using UnityEngine;

public class MovingPlatform : Subject, IInteract
{
    [SerializeField] private PlanformMover m_movePlatform;
    [SerializeField] private int m_priority = 3;
    [SerializeField] private Transform m_intarectionPoint;
    [SerializeField] private string m_displayName = "Activate Platform";

    public int priority => m_priority;
    public Transform position => m_intarectionPoint != null ? m_intarectionPoint : transform;
    public string displayName => m_displayName;

    private void Awake()
    {
        m_movePlatform?.Initialize();
    }

    public void Interact()
    {
        if (!CanInteract())
        {
            return;
        }

        m_movePlatform.Activate();
        NotifyObservers();
    }

    public bool CanInteract() =>
        m_movePlatform != null && m_movePlatform.CanMove();

    public void OnFocusGained()
    {
    }

    public void OnFocusLost()
    {
    }
}
