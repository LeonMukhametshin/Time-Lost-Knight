using Game.Buffs.Interfaces;
using Game.Core.CoreComponents;
using Game.Core.ServiceLocatorSpace;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Projectiles
{
    [MovedFrom("")]
    public abstract class BaseProjectile : MonoBehaviour, IProjectile
    {
        [SerializeReference][SerializeReferenceDropdown] protected IEffect[] m_effects;

        [SerializeField] protected Rigidbody2D projectileRigidbody;

        [SerializeField] private GameObject m_vfx;

        private float m_speed;

        private Vector3 m_direction;

        private bool m_initialized;

        private void OnValidate()
        {
            if (!projectileRigidbody)
            {
                projectileRigidbody = GetComponent<Rigidbody2D>();
            }
        }

        protected virtual void Awake()
        {
            projectileRigidbody.gravityScale = 0f;
            projectileRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        public virtual void Initialize(Vector2 targetPosition, float speed)
        {
            if(m_initialized)
            {
                return;
            }

            m_direction = (targetPosition - (Vector2)transform.position).normalized;
            m_speed = speed;

            float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            SetLinearVelocity();
            m_initialized = true;
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            HitObject(collision);
            DestroyProjectile();
        }

        protected void HitObject(Collider2D collision)
        {
            if (collision.TryGetComponent<CoreSystem>(out var core))
            {
                if (core != null && m_effects != null)
                {
                    m_effects.ApplyEffect(core.effectables);
                }
            }
        }

        private void SetLinearVelocity() =>
            projectileRigidbody.linearVelocity = m_direction * m_speed;

        protected virtual void DestroyProjectile()
        {
            ServiceLocator
                .Get<ParticleManager>()
                .StartParticlesWithRandomRotation(m_vfx, transform.position);

            Destroy(gameObject);
        }
    }
}
