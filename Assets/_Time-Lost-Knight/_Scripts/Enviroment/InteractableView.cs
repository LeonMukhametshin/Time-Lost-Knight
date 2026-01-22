using TMPro;
using UnityEngine;

public class InteractableView : MonoBehaviour
{
    [SerializeField] private string m_text;
    [SerializeField] private TextMeshProUGUI m_textMeshPro;
    [SerializeField] private InteractionDetector m_interactionDetector;

    private void OnValidate()
    {
        if(!m_textMeshPro)
        {
            m_textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        m_textMeshPro.text = m_text;
        SetTextState(false);
    }

    private void OnEnable()
    {
        m_interactionDetector.IsInInteractRange += SetTextState;
    }

    private void OnDisable()
    {
        m_interactionDetector.IsInInteractRange -= SetTextState;
    }

    private void SetTextState(bool active)
    {
        m_textMeshPro.enabled = active;
    }
}