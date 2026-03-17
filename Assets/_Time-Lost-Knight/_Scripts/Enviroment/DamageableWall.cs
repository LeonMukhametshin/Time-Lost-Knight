using Game.Buffs.Interfaces;
using Game.Core.CoreComponents;
using Game.Core.ServiceLocatorSpace;
using Game.Effects;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Environment
{
    [MovedFrom("")]
    public class DamageableWall : MonoBehaviour, IHealth, IEffectable, IDamageable
    {
        public event Action died;
        public event Action valueChanged;

        [SerializeField][Min(0)] private float m_maxHealt;
        [SerializeField] private GameObject m_destroyParticle;

        private float m_value;
        private bool m_isInitialized;

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

        private void Awake() =>
            Initialize(m_maxHealt);

        private void OnEnable()
        {
            died += Destroy;
        }

        private void OnDisable()
        {
            died -= Destroy;
        }

        public void Initialize(float maxHealth)
        {
            if (m_isInitialized)
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


        private void Destroy()
        {
            ServiceLocator
                .Get<ParticleManager>()
                .StartParticlesWithRandomRotation(m_destroyParticle, transform.position);
            Destroy(this.gameObject);
        }
    }
}

