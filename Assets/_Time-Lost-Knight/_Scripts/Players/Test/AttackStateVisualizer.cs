using TMPro;
using UnityEngine;

public class AttackStateVisualizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private AttackSystem m_attackSystem;

    private void OnValidate()
    {
        if (!m_text)
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        //m_attackSystem.StateCnanged += UpdateText;
    }

    private void OnDisable()
    {
        //m_attackSystem.StateCnanged -= UpdateText;
    }

    private void Awake()
    {
        UpdateText(AttackState.Idle);
    }

    private void UpdateText(AttackState states) =>
        m_text.text = states.ToString();
}