using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D m_rigidbody;

    [SerializeField] protected float m_health;
    [SerializeField] protected float m_recoilLenght;
    [SerializeField] protected float m_recoilFactor;
    [SerializeField] protected bool m_isRecoiling = false;

    [SerializeField] protected Transform m_target;
    [SerializeField] protected float m_speed = 5f;
    [SerializeField] protected float m_damage;
    protected float m_recoilTimer;

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if(m_isRecoiling)
        {
            if(m_recoilTimer < m_recoilFactor)
            {
                m_recoilTimer += Time.deltaTime;
            }
            else
            {
                m_isRecoiling = false;
                m_recoilTimer = 0;
            }
        }
    }

    public virtual void Hit(float damage, Vector2 hitDirection, float hitForce)
    {
        m_health -= damage;

        if(!m_isRecoiling)
        {
            m_rigidbody.AddForce((-hitForce * m_recoilFactor) * hitDirection);
        }

        if(m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.GetComponent<TestPlayerController>().m_playerStateList.Invincible)
        {
            Attack(collision.gameObject);
        }
    }

    protected virtual void Attack(GameObject objectToAttack)
    {
        objectToAttack.GetComponent<TestPlayerController>().TakeDamage(m_damage);
    }
}
