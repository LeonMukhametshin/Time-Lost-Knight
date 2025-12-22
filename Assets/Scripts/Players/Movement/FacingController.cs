using UnityEngine;

public class FacingController
{
    private readonly Transform m_transform;
    private readonly MovementState m_state;

    public FacingController(Transform transform, MovementState state)
    {
        m_transform = transform;
        m_state = state;
    }

    public void UpdateFacing(float xInput)
    {
        if (xInput == 0) return;

        if (xInput > 0 && !m_state.IsFacingRight ||
            xInput < 0 && m_state.IsFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_state.Flip();
        Vector3 scale = m_transform.localScale;
        scale.x *= -1;
        m_transform.localScale = scale;
    }
}