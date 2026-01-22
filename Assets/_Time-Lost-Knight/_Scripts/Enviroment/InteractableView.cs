using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableView : MonoBehaviour
{
    [SerializeField] private string m_text;
    [SerializeField] private Sprite m_sprite;

    [SerializeField] private TextMeshProUGUI m_textMeshPro;
    [SerializeField] private Image m_spriteRenderer;
    [SerializeField] private InteractionDetector m_interactionDetector;

    [SerializeField] private GameObject m_gameObject;

    private void Awake()
    {
        m_textMeshPro.text = m_text;
        m_spriteRenderer.sprite = m_sprite;

        SetInteractableViewState(false);
    }

    private void OnEnable() =>
         m_interactionDetector.IsInInteractRange += SetInteractableViewState;

    private void OnDisable() =>
        m_interactionDetector.IsInInteractRange -= SetInteractableViewState;

    private void SetInteractableViewState(bool active) =>
         m_gameObject.SetActive(active);
}