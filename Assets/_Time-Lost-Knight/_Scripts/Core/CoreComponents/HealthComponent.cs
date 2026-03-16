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

        public float maxValue { get; private set; }

        public float value
        {
            get => m_value;
            private set
            {
                if (Mathf.Approximately(m_value, value))
                {
                    return;
                }
                m_value = value < 0 ? 0 : value;

                valueChanged?.Invoke();

                if (value >= maxValue)
                {
                    return;
                }

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
            this.value -= damage;
        }
    }
}
