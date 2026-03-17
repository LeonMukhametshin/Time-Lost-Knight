using UnityEngine;

public class Death : CoreComponent
{
    private HealthComponent m_healthComponent;
    protected HealthComponent healthComponent =>
        m_healthComponent ??= core.GetCoreComponent<HealthComponent>();

    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_deathClip;
    [SerializeField][Range(0f, 1f)] private float m_deathVolume = 1f;

    private void OnEnable() =>
        healthComponent.died += Die;

    private void OnDisable() =>
        healthComponent.died -= Die;

    private void Die()
    {
        DetachedAudioPlayer.Play(m_audioSource, m_deathClip, m_deathVolume);

        ServiceLocator
            .Get<ParticleManager>()
            .StartParticles(deathParticles, transform.position, Quaternion.identity);

        this.gameObject.SetActive(false);  
    }
}

internal static class DetachedAudioPlayer
{
    public static void Play(AudioSource template, AudioClip clip, float volume)
    {
        if (template == null || clip == null)
        {
            return;
        }

        GameObject audioObject = new($"{clip.name} One Shot");
        audioObject.transform.position = template.transform.position;

        AudioSource source = audioObject.AddComponent<AudioSource>();
        ApplyTemplate(template, source);
        source.clip = clip;
        source.loop = false;
        source.playOnAwake = false;
        source.volume = Mathf.Clamp01(volume);
        source.Play();

        Object.Destroy(audioObject, clip.length / Mathf.Max(Mathf.Abs(source.pitch), 0.01f) + 0.1f);
    }

    private static void ApplyTemplate(AudioSource template, AudioSource source)
    {
        source.outputAudioMixerGroup = template.outputAudioMixerGroup;
        source.priority = template.priority;
        source.pitch = template.pitch;
        source.panStereo = template.panStereo;
        source.mute = template.mute;
        source.spatialBlend = template.spatialBlend;
        source.spread = template.spread;
        source.reverbZoneMix = template.reverbZoneMix;
        source.rolloffMode = template.rolloffMode;
        source.minDistance = template.minDistance;
        source.maxDistance = template.maxDistance;
        source.bypassEffects = template.bypassEffects;
        source.bypassListenerEffects = template.bypassListenerEffects;
        source.bypassReverbZones = template.bypassReverbZones;
        source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, template.GetCustomCurve(AudioSourceCurveType.CustomRolloff));
        source.SetCustomCurve(AudioSourceCurveType.SpatialBlend, template.GetCustomCurve(AudioSourceCurveType.SpatialBlend));
        source.SetCustomCurve(AudioSourceCurveType.Spread, template.GetCustomCurve(AudioSourceCurveType.Spread));
        source.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, template.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
    }
}
