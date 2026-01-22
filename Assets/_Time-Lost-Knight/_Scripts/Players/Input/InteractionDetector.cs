using System;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    public event Action<bool> IsInInteractRange;
   
    private IInteractable m_interactableInRange = null;

    private bool m_canInteract = true;
    public bool canInteract
    {
        get => m_canInteract;
        set 
        {
            if (m_canInteract != value)
            {
                m_canInteract = value;
                IsInInteractRange?.Invoke(m_canInteract);
            }
        }
    }

    public void OnInteract()
    {
        m_interactableInRange?.Interact();
    }
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            m_interactableInRange = interactable;
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == m_interactableInRange)
        {
            m_interactableInRange = null;
            canInteract = false;
        }
    }
}