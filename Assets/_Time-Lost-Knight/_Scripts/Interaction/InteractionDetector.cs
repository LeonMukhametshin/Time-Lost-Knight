using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class InteractionDetector : MonoBehaviour
{
    public event Action InteractablesChanged;

    private List<IInteractable> m_interactables = new();
    public List<IInteractable> Interactables => m_interactables;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out IInteractable interactable))
        {
             return;
        }

        if (!m_interactables.Contains(interactable))
        {
            m_interactables.Add(interactable);
            InteractablesChanged?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out IInteractable interactable))
        {
            return;
        }

        if (m_interactables.Remove(interactable))
        {
            InteractablesChanged?.Invoke();
        }
    }
}