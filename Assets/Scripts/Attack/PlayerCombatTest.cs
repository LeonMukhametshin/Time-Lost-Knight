using Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatTest : MonoBehaviour
{
    [SerializeField] private WeaponConfig weaponConfig;
    [SerializeField] private CritConfig critConfig;

    [SerializeField] private MeleeAttack meleeAttack;

    private IWeapon m_weapon;

    private void Awake()
    { 
        m_weapon = WeaponFactory.CreateBaseWeapon(weaponConfig);
        meleeAttack.SetWeapon(m_weapon);

        critConfig.Init(weaponConfig);
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            meleeAttack.PerformAttack();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            print("Update to crit");
            m_weapon = WeaponFactory.AddCritical(
                m_weapon,
                critConfig);

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