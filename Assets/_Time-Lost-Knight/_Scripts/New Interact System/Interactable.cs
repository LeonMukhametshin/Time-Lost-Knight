using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteract
{
    [SerializeField] private string m_displayName = "Interact";
    [SerializeField] private bool m_isEnabled = true;
    [SerializeField] private Outline m_outline;
    [SerializeField] private UnityEvent m_onIntarect;

    public Transform position => transform;

    public string displayName => m_displayName;

    private void Awake()
    {
        m_outline.OutlineMode = Outline.Mode.OutlineVisible;
        m_outline.OutlineColor = Color.red;
        m_outline.OutlineWidth = 1f;
        m_outline.enabled = false;
    }

    public bool CanInteract() =>
        m_isEnabled;

    public void Interact() => 
        m_onIntarect?.Invoke();

    public void OnFocusGained() => 
        m_outline.enabled = true;

    public void OnFocusLost() => 
        m_outline.enabled = false;
}