using UnityEngine;

public class EntityCombat : Combat, IKnockbackable, IUpdate
{
    [SerializeField] private float maxKnockbackTime = 0.2f;
    [SerializeField] private GameObject damageParticles;

    [SerializeField] private Movement m_movement;
    [SerializeField] private ParticleManager m_particleManager;
    [SerializeField] private CollisionDetector m_collisionDetector;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void Awake()
    {
        base.Awake();
        core.AddUpdateComponent(this);
    }

    public void Update()
    {
        CheckKnockback();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        m_particleManager.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        m_movement.SetVelocity(strength, angle, direction);
        KnockbackSetParameters();
    }

    public void Knockback(Vector2 angle, float strength)
    {
        m_movement.SetVelocity(strength, angle);
        KnockbackSetParameters();
    }

    private void KnockbackSetParameters()
    {
        m_movement.canSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive
            && ((m_movement.currentVelocity.y <= 0.01f
            && m_collisionDetector.CheckGrounded())
            || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            m_movement.canSetVelocity = true;
        }
    }
}