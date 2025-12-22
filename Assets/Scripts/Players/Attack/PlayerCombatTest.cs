using Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatTest : MonoBehaviour
{
    [SerializeField] private Transform m_attackPoint;
    [SerializeField] private Vector2 m_attackArea;
    [SerializeField] private WeaponConfig m_weaponConfig;
    [SerializeField] private CritConfig m_critConfig;

    private MeleeAttack meleeAttack;

    private IWeapon m_weapon;

    private void Awake()
    { 
        m_weapon = WeaponFactory.CreateBaseWeapon(m_weaponConfig);
        meleeAttack = new MeleeAttack(m_weapon, m_attackPoint, m_attackArea);
        m_critConfig.Initialize(m_weaponConfig);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            meleeAttack.PerformAttack();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            print("Update to crit");
            m_weapon = WeaponFactory.AddCritical(
                m_weapon,
                m_critConfig);

            meleeAttack.SetWeapon(m_weapon);
        }

        if(Keyboard.current.xKey.wasPressedThisFrame)
        {
            for(int i = 0; i < 1000; i++)
            {
                meleeAttack.PerformAttack();
            }
        }
    }
}