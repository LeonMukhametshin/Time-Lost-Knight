using UnityEngine.InputSystem;

public sealed class AttackInputHandler
{
    private readonly PlayerAttackSystem m_attackSystem;
    private readonly GameInput m_input;

    public AttackInputHandler(
        GameInput input,
        PlayerAttackSystem attackSystem,
        CoroutineRunner coroutine)
    {
        m_attackSystem = attackSystem;
        m_attackSystem.Initialize(coroutine);

        m_input = input;

        Bind(m_input.Player.MainWeaponAttack1, AttackSlot.Main);
        Bind(m_input.Player.MainWeaponAttack2, AttackSlot.Additional);
        Bind(m_input.Player.AdditionalWeaponAttack1, AttackSlot.AbilityQ);
        Bind(m_input.Player.AdditionalWeaponAttack2, AttackSlot.AbilityE);
    }

    private void Bind(InputAction action, AttackSlot slot)
    {
        action.performed += _ => m_attackSystem.Attack(slot);
    }
}