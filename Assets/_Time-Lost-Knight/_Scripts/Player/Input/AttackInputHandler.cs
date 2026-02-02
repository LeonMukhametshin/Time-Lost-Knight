using System;
using UnityEngine;

[Serializable]
public sealed class AttackInputHandler
{
    [SerializeField] private PlayerAttackSystem m_attackSystem;

    private GameInput m_gameInput;
    private bool m_isInitialized;

    public void Update()
    {
        if(!m_isInitialized)
        {
            return;
        }

        if(m_gameInput.Player.MainWeaponAttack1.WasPerformedThisFrame())
        {
            m_attackSystem.Attack(AttackSlot.Main);
        }
        else if (m_gameInput.Player.MainWeaponAttack2.WasPerformedThisFrame())
        {
            m_attackSystem.Attack(AttackSlot.Additional);
        }
        else if(m_gameInput.Player.AdditionalWeaponAttack1.WasPerformedThisFrame())
        {
            m_attackSystem.Attack(AttackSlot.AbilityQ);
        }
        else if(m_gameInput.Player.AdditionalWeaponAttack2.WasPerformedThisFrame())
        {
            m_attackSystem.Attack(AttackSlot.AbilityE);
        }
    }

    public void Initialize(CoroutineRunner coroutine)
    {
        m_gameInput = new GameInput();
        m_gameInput.Player.Enable();

        m_attackSystem.Initialize(coroutine);

        m_isInitialized = true;
    }
}