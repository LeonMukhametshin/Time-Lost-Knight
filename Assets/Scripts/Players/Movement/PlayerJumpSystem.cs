using UnityEngine;

namespace Players
{
    public class PlayerJumpSystem
    {
        private Rigidbody2D m_rigidbody;
        private GroundCheck m_groundCheck;

        private readonly float m_jumpForce;
        private readonly float m_jumpHoldForce;
        private readonly float m_maxHoldTime;

        private float m_jumpTimer;
        private bool m_isJumping;
        private bool m_isHoldingJump;

        public PlayerJumpSystem(
            Rigidbody2D rigidbody,
            GroundCheck groundCheck,
            float jumpForce,
            float jumpHoldForce = 2f, 
            float maxHoldTime = 0.3f) 
        {
            m_rigidbody = rigidbody;
            m_groundCheck = groundCheck;
            m_jumpForce = jumpForce;
            m_jumpHoldForce = jumpHoldForce;
            m_maxHoldTime = maxHoldTime;
        }

        public void StartJump()
        {
            if (m_groundCheck.IsGrounded && !m_isJumping)
            {
                m_rigidbody.linearVelocity = new Vector2(
                    m_rigidbody.linearVelocityX,
                    0f
                );

                m_rigidbody.AddForce(
                    Vector2.up * m_jumpForce,
                    ForceMode2D.Impulse
                );

                m_isJumping = true;
                m_isHoldingJump = true;
                m_jumpTimer = 0f;
            }
        }

        public void StopJump()
        {
            m_isHoldingJump = false;

            if (m_rigidbody.linearVelocityY > 0)
            {
                m_rigidbody.linearVelocity = new Vector2(
                    m_rigidbody.linearVelocityX,
                    m_rigidbody.linearVelocityY * 0.5f
                );
            }
        }

        public void Update(float deltaTime)
        {
            if (m_isJumping)
            {
                if (m_groundCheck.IsGrounded)
                {
                    EndJump();
                    return;
                }

                if (m_isHoldingJump)
                {
                    m_jumpTimer += deltaTime;

                    if (m_jumpTimer < m_maxHoldTime && m_rigidbody.linearVelocityY > 0)
                    {
                        m_rigidbody.AddForce(
                            Vector2.up * (m_jumpHoldForce * deltaTime),
                            ForceMode2D.Force
                        );
                    }
                    else
                    {
                        m_isHoldingJump = false;
                    }
                }

                if (m_rigidbody.linearVelocityY < 0)
                {
                    m_isHoldingJump = false;
                }
            }
        }

        private void EndJump()
        {
            m_isJumping = false;
            m_isHoldingJump = false;
            m_jumpTimer = 0f;
        }

        public void Jump()
        {
            StartJump();
        }
    }
}