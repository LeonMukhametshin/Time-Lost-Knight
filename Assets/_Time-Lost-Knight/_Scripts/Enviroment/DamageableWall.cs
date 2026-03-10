using System;
using UnityEngine;

public class DamageableWall : MonoBehaviour, IHealth, IEffectable, IDamageable
{
    public event Action died;
    public event Action valueChanged;

    [SerializeField][Min(0)] private float m_maxHealt;
    [SerializeField] private GameObject m_destroyParticle;
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

    private float m_value;

    private bool m_isInitialized;

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