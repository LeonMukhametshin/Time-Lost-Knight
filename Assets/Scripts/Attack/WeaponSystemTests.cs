using Attacks;
using UnityEngine;

public class WeaponSystemTests : MonoBehaviour
{
    #region OldTest
    [SerializeField] private int m_wallHealth;
    [SerializeField] private WeaponConfig m_weaponConfig;
    [SerializeField] private CritWeaponConfig m_critConfig;

    private IWeapon weapon;
    private ICanBeDamageable m_damageable;
    private IDamageCalculator m_damageCalculator;

    private void Awake()
    {
        weapon = new Weapon(m_weaponConfig, DamageType.Physical);
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
            Debug.Log(m_damageCalculator);
            weapon = new CritDamageDecorator(weapon, m_damageCalculator, m_critConfig);
            Debug.Log("Update to CriticalDamageDecorator");
        }
    }
    #endregion
}