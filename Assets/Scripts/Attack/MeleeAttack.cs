using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform m_attackPoint;
    [SerializeField] private Vector2 m_attackArea;
    [SerializeField] private LayerMask m_damageableLayer;

    private IWeapon _weapon;

    public void SetWeapon(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public void PerformAttack()
    {
        var hits = Physics2D.OverlapBoxAll(
            m_attackPoint.position,
            m_attackArea,
            0f,
            m_damageableLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out ICanBeDamageable damageable))
            {
                _weapon.ApplyDamage(damageable);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_attackPoint.position, m_attackArea);
    }
}
