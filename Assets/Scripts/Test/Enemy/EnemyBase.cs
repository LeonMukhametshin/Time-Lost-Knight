using UnityEngine;

public class EnemyBase : IMovable
{
    protected Health m_health;
    protected float m_speed { get; set; }

    public EnemyBase(Health health)
    {
        m_health = health;
    }

    protected virtual void Move()
    {

    }

    protected virtual void Attack()
    {

    }

    protected void OnTriggerStay2D(Collider2D onther)
    {

    }
}   