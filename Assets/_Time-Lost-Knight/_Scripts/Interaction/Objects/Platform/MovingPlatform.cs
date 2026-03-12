using UnityEngine;

public class MovingPlatform : Subject
{
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private PlanformMover m_movePlatform;
   
    private void Awake()
    {
        m_movePlatform?.Initialize(m_rigidbody, transform);
    }

    public void FixedUpdate()
    {
        m_movePlatform?.FixedTick();
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
}
