using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour, IProjectile
{
    protected IReadOnlyList<IEffect> m_effects;

    [SerializeField] protected Rigidbody2D projectileRigidbody;

    [SerializeField] private GameObject m_vfx;

    private float m_speed;
    private float m_targetDistance;

    private Vector3 m_direction;
    private Vector3 m_startPosition;

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

    public virtual void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects)
    {
        m_startPosition = transform.position;

        Vector3 toTarget = targetPosition - m_startPosition;
        m_targetDistance = toTarget.magnitude;

        m_direction = (targetPosition - transform.position).normalized;
        m_speed = speed;
        m_effects = effects;

        SetLinearVelocity();

        m_initialized = true;
    }

    protected virtual void FixedUpdate()
    {
        if (!m_initialized)
        {
            return;
        }

        SetLinearVelocity();

        float traveledDistance = Vector3.Distance(m_startPosition, transform.position);

        if (traveledDistance >= m_targetDistance)
        {
            DestroyProjectile();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        HitObject(collision);
        DestroyProjectile();
    }

    protected void HitObject(Collider2D collision)
    {
        if (collision.TryGetComponent<Core>(out var core))
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