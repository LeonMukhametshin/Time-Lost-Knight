using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable, IUpdate
{
    [SerializeField] private GameObject damageParticles;

    private Stats m_stats;
    private Movement m_movement;
    private ParticleManager m_particleManager;
    private CollisionDetector m_collisionDetector;

    [SerializeField] private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void Awake()
    {
        base.Awake();
        m_stats = core.GetCoreComponent<Stats>();
        m_movement = core.GetCoreComponent<Movement>();
        m_particleManager = core.GetCoreComponent<ParticleManager>();
        m_collisionDetector = core.GetCoreComponent<CollisionDetector>();
        core.AddUpdateComponent(this);
    }

    public void Update()
    {
        CheckKnockback();
    }

    public void TakeDamage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        m_stats.DecreaseHealth(amount);
        m_particleManager.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        m_movement.SetVelocity(strength, angle, direction);
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