using UnityEngine;

namespace Players
{
    public class PlayerJumpSystem
    {
        private Rigidbody2D m_rigidbody;
        private GroundCheck m_groundCheck;
        private float m_jumpForce;

        public PlayerJumpSystem(Rigidbody2D rigidbody, GroundCheck groundCheck, float jumpForce)
        {
            m_rigidbody = rigidbody;
            m_groundCheck = groundCheck;
            m_jumpForce = jumpForce;
        }

        public void Jump()
        {
            if (m_groundCheck.IsGrounded)
            {
                m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}