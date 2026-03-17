using Game.Buffs.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.CoreComponents
{
    [MovedFrom("")]
    public class HealthComponent : CoreComponent, IHealth, IEffectable
    {
        public event Action died;
        public event Action valueChanged;

        private float m_value;
        private bool m_isInitialized;
        private bool m_isInvincible;

        public float maxValue { get; private set; }

        public float value
        {
            get => m_value;
            private set
            {
                var clampedValue = maxValue > 0f
                    ? Mathf.Clamp(value, 0f, maxValue)
                    : Mathf.Max(0f, value);

                if (Mathf.Approximately(m_value, clampedValue))
                {
                    return;
                }
                m_value = clampedValue;

                valueChanged?.Invoke();

                if (m_value == 0)
                {
                    died?.Invoke();
                }
            }
        }

        public void Initialize(float maxHealth)
        {
            if(m_isInitialized)
            {
                return;
            }

            maxValue = maxHealth;
            value = maxHealth;
            m_isInitialized = true;
        }

        public void Heal(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Heal cannot be hegative");
            }

            this.value += value;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Heal cannot be hegative");
            }
            if (m_isInvincible)
            {
                return;
            }
            this.value -= damage;
        }

        public void SetInvincible(bool value) =>
            m_isInvincible = value;

        public void ToggleInvincible() =>
            m_isInvincible = !m_isInvincible;

        public bool isInvincible =>
            m_isInvincible;
    }
}
