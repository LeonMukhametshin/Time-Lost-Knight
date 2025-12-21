using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_boxCollider;
    [SerializeField] private LayerMask m_playerLayer;
    [SerializeField] private float m_currentCooldown;

    [SerializeField] private Transform[] m_transformPoints;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private Transform m_groundCheckPoint;
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private GroundCheckData m_groundCheckData;

    public HealthSystem m_healt { get; private set; }
    public IMovement m_movement { get; private set; }
    public EnemyState m_currentState { get; private set; }
    public GroundCheck m_groundChecker { get; private set; }
    public Transform m_transform { get; private set; }

    public Rigidbody2D Rigidbody2D => m_rigidbody2D;
    public EnemyData enemyData => m_enemyData;
    public Transform[] transformPoints => m_transformPoints;

    private void Awake()
    {
        InitializeComponent();
    }

    private void Update()
    {
        m_movement?.Update();

        m_currentCooldown -= Time.deltaTime;

        if (IsPlayerInSight())
        {
            if (m_currentCooldown <= 0)
            {
                m_currentCooldown = m_enemyData.m_attackCooldown;
                Debug.Log("Damaged");
                Attack();
            }
        }
    }

    private void InitializeComponent()
    {
        m_transform = transform;
        m_groundChecker = new GroundCheck(m_groundCheckData, m_transform, m_groundCheckPoint);
        m_movement = new PatrolEnemy(this);
        m_healt = new HealthSystem(m_enemyData.m_maxHealt, m_enemyData.m_initialHealth);
        m_currentCooldown = m_enemyData.m_attackCooldown;

        m_healt.Death += Death;
    }

    private bool IsPlayerInSight()
    {
        Vector3 boxColliderSize = m_boxCollider.bounds.size;

        var boxCenter = m_boxCollider.bounds.center +
            Vector3.right * (m_enemyData.m_attackRange * m_transform.localScale.x * m_enemyData.m_colliderDistanceMultiplier);

        Vector2 boxSize = new Vector2(
            boxColliderSize.x * m_enemyData.m_attackRange,
            boxColliderSize.y
        );

        RaycastHit2D hit = Physics2D.BoxCast(
            boxCenter,
            boxSize,
            0f,
            Vector2.left,
            0f,
            m_playerLayer
        );

        return hit.collider != null;
    }

    private void Attack()
    {
        m_currentCooldown = m_enemyData.m_attackCooldown;
        Debug.Log($"Attacked player for {m_enemyData.m_damage} damage");
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_boxCollider.bounds.center + transform.right * m_enemyData.m_attackRange * transform.localScale.x * m_enemyData.m_colliderDistanceMultiplier,
            new Vector3(m_boxCollider.bounds.size.x * m_enemyData.m_attackRange, m_boxCollider.bounds.size.y, m_boxCollider.bounds.size.z));
    }
}
