using Attacks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystemTests : MonoBehaviour
{
    #region OldTest
    [SerializeField] private WeaponConfig m_weaponConfig;
    [SerializeField] private CritWeaponConfig m_critConfig;

    [SerializeField] private Transform m_sideAttackTransform;
    [SerializeField] private Vector2 m_sideAttackArea;
    [SerializeField] private LayerMask m_damageableLayer;

    private IWeapon weapon;
    private IDamageCalculator m_damageCalculator;

    private void Awake()
    {
        weapon = new Weapon(m_weaponConfig, DamageType.Physical);
        m_damageCalculator = new CalculateCritDamage();
    }

    private void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Hit");
            Hit(m_sideAttackTransform, m_sideAttackArea);
        }
        if(Keyboard.current.qKey.wasPressedThisFrame)
        {
            weapon = new CritDamageDecorator(weapon, m_damageCalculator, m_critConfig);
            Debug.Log("Update to CriticalDamageDecorator");
        }
    }

    private void Hit(Transform attackTransform, Vector2 attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(attackTransform.position, attackArea, 0f, m_damageableLayer);
        
        foreach(var objectToHit in objectsToHit)
        {
            Debug.Log(objectToHit.name);
            if (objectToHit.TryGetComponent(out ICanBeDamageable damageable))
            {
                Debug.Log("damageable     " + objectToHit.name);
                weapon.ApplyDamage(damageable);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_sideAttackTransform.position, m_sideAttackArea);
    }
    #endregion
}