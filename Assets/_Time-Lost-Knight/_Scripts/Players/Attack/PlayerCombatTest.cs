using Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatTest : MonoBehaviour
{
    [SerializeField] private Transform m_attackPoint;
    [SerializeField] private MeleeWeaponConfig m_weaponConfig;

    private MeleeAttack meleeAttack;

    private IWeapon m_weapon;

    private void Awake()
    { 
        m_weapon = WeaponFactory.CreateBaseWeapon(m_weaponConfig);
        meleeAttack = new MeleeAttack(m_weapon, m_attackPoint, m_weaponConfig.attackZone);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            meleeAttack.PerformAttack();
        }
    }
}