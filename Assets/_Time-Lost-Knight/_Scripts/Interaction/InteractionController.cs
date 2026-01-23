using System.Linq;
using UnityEngine;

public sealed class InteractionController : MonoBehaviour
{
    [SerializeField] private InteractionDetector m_detector;

    private IInteractable m_current;

    private void OnEnable() =>
        m_detector.InteractablesChanged += SelectBestInteractable;

    private void OnDestroy() =>
          m_detector.InteractablesChanged -= SelectBestInteractable;

    private void SelectBestInteractable()
    {
        m_current = m_detector.Interactables
          .Where(i => i.CanInteract())
          .OrderByDescending(i => i.priority)
          .ThenBy(i => Vector2.Distance(
              transform.position,
              i.interactionPoint.position))
          .FirstOrDefault();
    }

    public bool HasInteractable() =>
        m_current is not null && m_current.CanInteract();

    public void Interact()
    {
        if (!HasInteractable())
        {
            return;
        }

        m_current.Interact();
        SelectBestInteractable();
    }
}