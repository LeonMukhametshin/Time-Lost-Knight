using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text m_label;
    [SerializeField] private Vector3 m_worldOffset = new(0f, 1f, 0f);
    [SerializeField] private RectTransform m_canvesRectTransform;
    [SerializeField] private RectTransform m_labelRect;

    [SerializeField] private string m_keyHit = "[E]";

    private Camera cam;
    private Transform m_target;
    private Canvas canvas;

    private void Awake()
    {
        cam = Camera.main;
        Hide();
    }

    private void LateUpdate()
    {
        if (m_target == null)
        {
            return;
        }

        Vector3 worldPosition = m_target.position + m_worldOffset;

        m_labelRect.position = worldPosition;
        m_labelRect.rotation = Quaternion.identity;
    }

    public void Show(IInteract interactable)
    {
        if(interactable is null)
        {
            Hide();
            return;
        }

        m_target = interactable.position;
        //m_label.text = $"{m_keyHit}{interactable.displayName}";
        m_label.text = interactable.displayName;
        m_label.gameObject.SetActive(true);
    }

    public void Hide()
    {
        m_label.gameObject.SetActive(false);
        m_target = null;
    }
}