using UnityEngine;

public class EntityCombat : Combat, IKnockbackable, IUpdate
{
    [SerializeField] private float maxKnockbackTime = 0.2f;
    [SerializeField] private GameObject damageParticles;

    private Movement m_movement;
    private ParticleManager m_particleManager;
    private CollisionDetector m_collisionDetector;

    private Movement movement =>
    m_movement ??= core.GetCoreComponent<Movement>();

    private ParticleManager particleManager =>
        m_particleManager ??= core.GetCoreComponent<ParticleManager>();

    private CollisionDetector collisionDetector =>
        m_collisionDetector ??= core.GetCoreComponent<CollisionDetector>();

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public void Update()
    {
        CheckKnockback();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        particleManager.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        movement.SetVelocity(strength, angle, direction);
        KnockbackSetParameters();
    }

    public void Knockback(Vector2 angle, float strength)
    {
        movement.SetVelocity(strength, angle);
        KnockbackSetParameters();
    }

    private void KnockbackSetParameters()
    {
        movement.canSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive
            && ((movement.currentVelocity.y <= 0.01f
            && collisionDetector.CheckGrounded())
            || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            movement.canSetVelocity = true;
        }
    }
}