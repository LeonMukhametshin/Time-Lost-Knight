using UnityEngine;

[RequireComponent (typeof(PlayerInput))]
[RequireComponent(typeof(PlayerHorizontalMove))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private PlayerHorizontalMove m_playerHorizontalMove;

    private void OnValidate()
    {
        if (!m_playerInput)
        {
            m_playerInput.GetComponent<PlayerInput>();
        }
        if (!m_playerHorizontalMove)
        {
            m_playerHorizontalMove.GetComponent<PlayerHorizontalMove>();
        }
    }

    private void FixedUpdate()
    {
        m_playerHorizontalMove.Move(m_playerInput.MoveInput);
    }
}
