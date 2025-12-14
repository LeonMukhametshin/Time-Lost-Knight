using Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatTest : MonoBehaviour
{

    [SerializeField] private WeaponConfig weaponConfig;
    [SerializeField] private CritWeaponConfig critConfig;

    [SerializeField] private MeleeAttack meleeAttack;

    private IWeapon _weapon;
    private IDamageCalculator _damageCalculator;

    private void Awake()
    {
        _damageCalculator = new CalculateCritDamage();

        _weapon = WeaponFactory.CreateBaseWeapon(weaponConfig);
        meleeAttack.SetWeapon(_weapon);
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            meleeAttack.PerformAttack();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _weapon = WeaponFactory.AddCritical(
                _weapon,
                critConfig,
                _damageCalculator);

            meleeAttack.SetWeapon(_weapon);
        }
    }
}