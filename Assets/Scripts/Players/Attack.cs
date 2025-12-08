using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform m_sideAttackTransform;
    [SerializeField] private Transform m_upAttackTransform;
    [SerializeField] private Transform m_downAttackTransform;

    [SerializeField] private Vector2 m_sideAttackArea;
    [SerializeField] private Vector2 m_upAttackArea;
    [SerializeField] private Vector2 m_downAttackArea;

    [SerializeField] private LayerMask m_attackableLayer;
    private float m_timeBetweenAttack = 1f;
    private float m_timeSinceAttack;

    public void DoAttack()
    {
        m_timeSinceAttack += Time.deltaTime;
        if (m_timeSinceAttack >= m_timeBetweenAttack)
        {
            m_timeSinceAttack = 0;
        }
    }

    private void Hit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(attackTransform.position, attackArea, m_attackableLayer);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_sideAttackTransform.position, m_sideAttackArea);
        Gizmos.DrawWireCube(m_upAttackTransform.position, m_upAttackArea);
        Gizmos.DrawWireCube(m_downAttackTransform.position, m_downAttackArea);
    }
}