using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerHorizontalMove : MonoBehaviour
    {
        [SerializeField] private PlayerData m_data;

        [SerializeField] private Rigidbody2D m_rigidbody2D;
        [SerializeField] private Transform m_transform;

        private float m_currentDirection = 1f;

        private void OnValidate()
        {
            if (!m_rigidbody2D)
            {
                m_rigidbody2D = GetComponent<Rigidbody2D>();
            }
            if (!m_transform)
            {
                m_transform = transform;
            }
        }

        public void Move(Vector2 moveDirection)
        {
            Vector2 velocity = moveDirection.normalized * m_data.Speed;
            m_rigidbody2D.linearVelocity = new Vector2(velocity.x, m_rigidbody2D.linearVelocityY);

            float horizontalInput = Mathf.Clamp(moveDirection.x, -1f, 1f);
            Flip(horizontalInput);
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