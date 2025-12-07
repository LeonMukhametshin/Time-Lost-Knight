using UnityEngine;

namespace Players
{
    public class GroundCheck
    {
        private Transform m_transform;
        private float m_checkDistance;
        private LayerMask m_groundLayer;

        public bool IsGrounded { get; private set; }

        public GroundCheck(Transform transform, float checkDistance, LayerMask groundLayer)
        {
            m_transform = transform;
            m_checkDistance = checkDistance;
            m_groundLayer = groundLayer;
        }

        public void Update()
        {
            if (m_transform == null)
            {
                return;
            }

            var hit = Physics2D.OverlapCircle(m_transform.position, m_checkDistance, m_groundLayer);

            IsGrounded = hit != null;

            Debug.Log(IsGrounded);

            Debug.DrawRay(m_transform.position, Vector2.down * m_checkDistance,
                         IsGrounded ? Color.green : Color.red);
        }
    }
}