using UnityEngine;

public class KeyboardPlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private PlayerInputConfig m_config;
    private bool m_jumpPressed;

    private void Update()
    {
        if (Input.GetKeyDown(m_config.JumpButton))
        {
            m_jumpPressed = true;
        }
    }

    public float GetHorizontalInput() => Input.GetAxis(m_config.HorizontalInputAxis);
    public bool IsJumpPressed() => m_jumpPressed;
    public bool IsJumpHeld() => Input.GetKey(m_config.JumpButton);
    public void ClearJumpInput() => m_jumpPressed = false;
    public bool IsDashPressed() => Input.GetKeyDown(m_config.DashButton);
}
