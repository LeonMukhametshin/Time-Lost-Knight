using UnityEngine;

public abstract class Combat : CoreComponent, IEffectable, IDamageable
{
    private HealthComponent m_healthComponent;
    protected HealthComponent healthComponent =>
        m_healthComponent ??= core.GetCoreComponent<HealthComponent>();

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_damageClip;
    [SerializeField][Range(0f, 1f)] private float m_damageVolume = 1f;

    public virtual void TakeDamage(float amount)
    {
        healthComponent?.TakeDamage(amount);

        if (m_audioSource == null || m_damageClip == null || healthComponent == null || healthComponent.value <= 0f)
        {
            return;
        }

        DetachedAudioPlayer.Play(m_audioSource, m_damageClip, m_damageVolume);
    }
}
