using Attacks;
using UnityEngine;

public class TestAttackSystem : MonoBehaviour
{
    [SerializeField] private int m_wallHealth;
    [SerializeField] private WeaponConfig m_weaponConfig;
    [SerializeField] private DamageType m_damageType;

    private IWeapon weapon;
    private ICanBeDamageable m_damageable;
    private IDamageCalculator m_damageCalculator;

    private void Awake()
    {
        weapon = new Weapon(m_weaponConfig, m_damageType);
        m_damageable = new DamageableWall(m_wallHealth);
        m_damageCalculator = new CalculateCritDamage();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weapon.ApplyDamage(m_damageable);
        }
        if(Input.GetMouseButtonDown(1))
        {
            weapon = new CriticalDamageDecorator(weapon, m_damageCalculator, m_weaponConfig);
            Debug.Log("Update to CriticalDamageDecorator");
        }
    }
}
