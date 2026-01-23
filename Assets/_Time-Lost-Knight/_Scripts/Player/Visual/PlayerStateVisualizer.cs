using TMPro;
using UnityEngine;

public class PlayerStateVisualizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private PlayerMovementController m_movementController;

    private void OnValidate()
    {
        if(!m_text)
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        m_movementController.StateChanged += UpdateText;
    }

    private void OnDisable()
    {
        m_movementController.StateChanged -= UpdateText;
    }

    private void Awake()
    {
        UpdateText(MovementStates.Idle);
    }

    private void UpdateText(MovementStates states) =>
        m_text.text = states.ToString();
}
