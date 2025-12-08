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
            IsGrounded = Grounded();
        }

        private bool Grounded()
        {
            if(Physics2D.OverlapCircle(m_transform.position, m_checkDistance, m_groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}