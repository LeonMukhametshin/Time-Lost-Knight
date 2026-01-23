using UnityEngine;

public class Chest : Subject, IInteractable
{
    [SerializeField] private int m_priority = 2;
    [SerializeField] private Transform m_intarectionPoint;

    public int priority => m_priority;
    public Transform interactionPoint => m_intarectionPoint;

    private bool m_isOpen = false;

    public bool CanInteract() =>
        !m_isOpen;

    public void Interact()
    {
        if(m_isOpen)
        {
            return;
        }

        m_isOpen = true;
        NotifyObservers();
    }
}