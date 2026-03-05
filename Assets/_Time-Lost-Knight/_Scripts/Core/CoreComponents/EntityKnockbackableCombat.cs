using UnityEngine;

public class EntityKnockbackableCombat : EntityCombat, IKnockbackable, IUpdate
{
    [SerializeField] private float m_maxKnockbackTime = 0.2f;

    private CollisionDetector collisionDetector =>
        m_collisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

    private Movement movement =>
        m_movement ??= core.GetCoreComponent<Movement>();


    private CollisionDetector m_collisionDetector;
    private Movement m_movement;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public void Update()
    {
        CheckKnockback();
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
            || Time.time >= knockbackStartTime + m_maxKnockbackTime))
        {
            isKnockbackActive = false;
            movement.canSetVelocity = true;
        }
    }
}