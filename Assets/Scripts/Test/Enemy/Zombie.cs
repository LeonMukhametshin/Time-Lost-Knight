using UnityEngine;

public class Zombie : Enemy
{
    private void Start()
    {
        m_rigidbody.gravityScale = 12f;
    }

    public override void Update()
    {
        base.Update();

        if(!m_isRecoiling)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(m_target.position.x, transform.position.y), m_speed * Time.deltaTime);
        }
    }

    public override void Hit(float damage, Vector2 hitDirection, float hitForce)
    {
        base.Hit(damage, hitDirection, hitForce);
    }
}