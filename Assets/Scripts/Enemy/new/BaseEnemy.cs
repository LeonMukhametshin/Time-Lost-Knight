using UnityEngine;
using static EnemyPatrol;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    public HealthSystem m_healt { get; private set; }
    public IMovement m_movement { get; private set; }
    public EnemyState m_currentState { get; private set; }
    public float findingPathTimer { get; private set; }

    [Header("Attack")]
    [SerializeField] private BoxCollider2D m_boxCollider;
    [SerializeField] private float m_colliderDistance;
    [SerializeField] private LayerMask m_playerLayer;
    [SerializeField] private float m_range;
    [SerializeField] private float m_currentCooldown;
    private void Awake()
    {
        InitializeComponent();
    }

    private void Update()
    {
        m_currentCooldown -= Time.deltaTime;

        if (PlayerInSight())
        {
            if (m_currentCooldown <= 0)
            {
                m_currentCooldown = enemyData.m_attackCooldown;
                Debug.Log("Damaged");
            }
        }
    }

    private void InitializeComponent()
    {
        m_healt = new HealthSystem(enemyData.m_maxHealt, enemyData.m_initialHealth);
        m_currentCooldown = enemyData.m_attackCooldown;

        m_healt.Death += Death;
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(m_boxCollider.bounds.center + transform.right * m_range * transform.localScale.x * m_colliderDistance,
            new Vector3(m_boxCollider.bounds.size.x * m_range, m_boxCollider.bounds.size.y, m_boxCollider.bounds.size.z),
            0, Vector2.left, 0, m_playerLayer);

        if (hit.collider != null)
            //hit.collider.GetComponent<IDamageable>().TakeDamage(enemyData.m_damage);
            //Debug.Log("Damaged");

        return hit.collider != null;
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_boxCollider.bounds.center + transform.right * m_range * transform.localScale.x * m_colliderDistance,
            new Vector3(m_boxCollider.bounds.size.x * m_range, m_boxCollider.bounds.size.y, m_boxCollider.bounds.size.z));
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }
}
