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

    private void Start()
    {
        m_movementController.m_fsm.stateChanged += UpdateText;
    }

    private void UpdateText(MovementState state) =>
        m_text.text = state.ToString();
}
