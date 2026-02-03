using System;
using UnityEngine;

namespace Inputs
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        private GameInput m_gameInput;

        public event Action<Vector2> move;
        public event Action jump;
        public event Action dash;
        public event Action interact;
        public event Action<AttackSlot> attack;

        private Vector2 m_lastInput = Vector2.zero;

        private bool m_isInitialize;

        private void OnDisable()
        {
            Unsubscribe();
            DeactivatePlayerInput();
        }

        public void Intialize()
        {
            if (m_isInitialize)
            {
                return;
            }

            m_gameInput = new GameInput();

            ActivatePlayerInput();
            Subscribe();
        }

        private void Subscribe()
        {
            m_gameInput.Player.Jump.performed += _ => jump?.Invoke();
            m_gameInput.Player.Dash.performed += _ => dash?.Invoke(); ;
            m_gameInput.Player.Interact.performed += _ => interact?.Invoke();

            m_gameInput.Player.MainWeaponAttack1.performed += _ => attack?.Invoke(AttackSlot.Main);
            m_gameInput.Player.MainWeaponAttack2.performed += _ => attack?.Invoke(AttackSlot.Additional);
            m_gameInput.Player.AdditionalWeaponAttack1.performed += _ => attack?.Invoke(AttackSlot.AbilityQ);
            m_gameInput.Player.AdditionalWeaponAttack2.performed += _ => attack?.Invoke(AttackSlot.AbilityE);
        }
        
        private void Unsubscribe()
        {
            m_gameInput.Player.Jump.performed -= _ => jump?.Invoke();
            m_gameInput.Player.Dash.performed -= _ => dash?.Invoke(); ;
            m_gameInput.Player.Interact.performed -= _ => interact?.Invoke();

            m_gameInput.Player.MainWeaponAttack1.performed -= _ => attack?.Invoke(AttackSlot.Main);
            m_gameInput.Player.MainWeaponAttack2.performed -= _ => attack?.Invoke(AttackSlot.Additional);
            m_gameInput.Player.AdditionalWeaponAttack1.performed -= _ => attack?.Invoke(AttackSlot.AbilityQ);
            m_gameInput.Player.AdditionalWeaponAttack2.performed -= _ => attack?.Invoke(AttackSlot.AbilityE);
        }

        public void Update()
        {
            Vector2 input = m_gameInput.Player.Move.ReadValue<Vector2>();

            if (input.sqrMagnitude > 0.01f)
            {
                move?.Invoke(input);
                m_lastInput = input;
            }
            else if(input != m_lastInput)
            {
                move?.Invoke(Vector2.zero);
                m_lastInput = Vector2.zero;
            }
        }
         
        public void ActivatePlayerInput() =>
            m_gameInput.Player.Enable();

        public void DeactivatePlayerInput() =>
           m_gameInput.Player.Disable();

        //TODO UI input 
    }
}
