using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float m_startHealthPointsValue = 100;
        [SerializeField] private float m_damage = 10f;

        private PlayerInput m_input;
        private Health m_health;

        private void Awake()
        {
            m_health = new Health(m_startHealthPointsValue);
            m_input = new PlayerInput(new InputSystemActions());
        }

        private void Update()
        {
            if(m_input.IsAttacked)
            {
                m_health.TakeDamage(m_damage);
            }
        }
    }
}