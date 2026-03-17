using Game.Buffs.Interfaces;
using Game.Core.CoreComponents;
using Game.Core.ServiceLocatorSpace;
using Game.Effects;
using System;
using UnityEngine;

public class DamageableWall : MonoBehaviour, IHealth, IEffectable, IDamageable
{
    public event Action died;
    public event Action valueChanged;

    [SerializeField][Min(0)] private float m_maxHealt;
    [SerializeField] private GameObject m_destroyParticle;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_destroyClip;
    [SerializeField][Range(0f, 1f)] private float m_destroyVolume = 1f;
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

    private void Awake()
    {
        EnsureAudioSource();
        Initialize(m_maxHealt);
    }

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
        DetachedAudioPlayer.Play(m_audioSource, m_destroyClip, m_destroyVolume);

        ServiceLocator
            .Get<ParticleManager>()
            .StartParticlesWithRandomRotation(m_destroyParticle, transform.position);
        Destroy(this.gameObject);
    }

    private void EnsureAudioSource()
    {
        if (m_audioSource != null)
        {
            return;
        }

        if (!TryGetComponent(out m_audioSource))
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
        }

        m_audioSource.playOnAwake = false;
        m_audioSource.loop = false;
        m_audioSource.priority = 160;
        m_audioSource.dopplerLevel = 0f;
        m_audioSource.spatialBlend = 0.35f;
        m_audioSource.minDistance = 8f;
        m_audioSource.maxDistance = 24f;
        m_audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
    }
}
