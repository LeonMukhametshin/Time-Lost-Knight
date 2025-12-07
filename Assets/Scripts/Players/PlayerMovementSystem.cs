using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementSystem 
    {
        private readonly PlayerData m_data;
        private readonly Rigidbody2D m_rigidbody2D;
        private readonly Transform m_transform;

        private float m_currentDirection = 1f;

        public PlayerMovementSystem(PlayerData data, Rigidbody2D rigidbody2D, Transform transform)
        {
            m_data = data;
            m_rigidbody2D = rigidbody2D;
            m_transform = transform;
        }

        public void Update(float input)
        {
            Move(input);
            Flip(Mathf.Clamp(input, -1f, 1f));
        }

        private void Move(float moveDirection)
        {
            m_rigidbody2D.linearVelocity = new Vector2(moveDirection * m_data.Speed, m_rigidbody2D.linearVelocityY); 
        }

        private void Flip(float moveDirectionX)
        {
            if (Mathf.Abs(moveDirectionX) < 0.1f)
            {
                return;
            }

            float targetDirection = Mathf.Sign(moveDirectionX);

            if (Mathf.Abs(targetDirection - m_currentDirection) > 0.1f)
            {
                m_currentDirection = targetDirection;

                Vector3 scale = m_transform.localScale;
                scale.x = Mathf.Abs(scale.x) * targetDirection;
                m_transform.localScale = scale;
            }
        }
    }
}