using Game.Audio;
using Game.Entities;
using Game.Player;
using UnityEngine;
using UnityEngine.Diagnostics;

[RequireComponent(typeof(AudioSource))]
public class StateAudioPlayer : MonoBehaviour
{
    [SerializeField] private Entity m_entity;
    [SerializeField] private StateAudioMapping m_mapping;
    [SerializeField] private bool m_stopIfNotMapped = true;
    [SerializeField] private bool m_useDistanceGate;
    [SerializeField] private string m_listenerTag = "Player";
    [SerializeField] private float m_startDistance = 5f;
    [SerializeField] private float m_stopDistance = 6f;

    [SerializeField] private AudioSource m_source;

    private string m_lastKey;
    private Transform m_listener;
    private AudioSource m_oneShotSource;

    private void Awake()
    {
        if (m_source == null)
        {
            m_source = GetComponent<AudioSource>();
        }

        if (m_entity == null)
        {
            m_entity = GetComponentInParent<Entity>();
        }

        m_oneShotSource = gameObject.AddComponent<AudioSource>();
        CopySourceSettings(m_source, m_oneShotSource);
    }

    private void Start()
    {
        if (m_entity is PlayerController && m_mapping != null && m_mapping.TryGet("PlayerIdleState", out var clip))
        {
            m_lastKey = "PlayerIdleState";
            PlayClip(clip);
        }
    }

    private void Update()
    {
        var state = m_entity?.fsm?.currentState;
        var key = state?.GetType().Name;

        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        if (m_mapping != null && m_mapping.TryGet(key, out var clip))
        {
            if (!CanPlayAtCurrentDistance(clip.loop))
            {
                if (m_source.isPlaying)
                {
                    m_source.Stop();
                }

                m_lastKey = key;
                return;
            }

            if (clip.manualTrigger)
            {
                if (key != m_lastKey)
                {
                    m_lastKey = key;

                    if (m_source.isPlaying)
                    {
                        m_source.Stop();
                    }
                }

                return;
            }

            if (key != m_lastKey)
            {
                if (clip.loop && m_source.isPlaying && !m_source.loop)
                {
                    return;
                }

                m_lastKey = key;
                PlayClip(clip);
            }
            else if (clip.loop && (m_source.clip != clip.clip || !m_source.isPlaying))
            {
                PlayClip(clip);
            }

            ApplyDistanceVolume(clip);
        }
        else if (m_stopIfNotMapped)
        {
            m_lastKey = key;
            m_source.Stop();
        }
    }

    private void PlayClip(StateClip clip)
    {
        if (clip.clip == null)
        {
            return;
        }

        m_source.clip = clip.clip;
        m_source.volume = clip.volume;
        m_source.loop = clip.loop;
        m_source.Play();
    }

    private void ApplyDistanceVolume(StateClip clip)
    {
        if (clip.clip == null || m_source == null)
        {
            return;
        }

        m_source.volume = clip.volume * GetDistanceVolumeMultiplier();
    }

    public void PlayOneShot(string stateKey)
    {
        if (m_mapping == null || string.IsNullOrWhiteSpace(stateKey))
        {
            return;
        }

        if (!m_mapping.TryGet(stateKey, out var clip) || clip.clip == null)
        {
            return;
        }

        if (!CanPlayAtCurrentDistance(false))
        {
            return;
        }

        m_oneShotSource.PlayOneShot(clip.clip, clip.volume * GetDistanceVolumeMultiplier());
    }

    private bool CanPlayAtCurrentDistance(bool isLoopClip)
    {
        if (!m_useDistanceGate)
        {
            return true;
        }

        if (m_listener == null && !TryResolveListener())
        {
            return false;
        }

        float distanceLimit = m_source.isPlaying && isLoopClip
            ? m_stopDistance
            : m_startDistance;

        Vector3 delta = m_listener.position - transform.position;
        delta.z = 0f;

        return delta.sqrMagnitude <= distanceLimit * distanceLimit;
    }

    private float GetDistanceVolumeMultiplier()
    {
        if (!m_useDistanceGate)
        {
            return 1f;
        }

        if (m_listener == null && !TryResolveListener())
        {
            return 0f;
        }

        Vector3 delta = m_listener.position - transform.position;
        delta.z = 0f;

        float distance = delta.magnitude;
        float minDistance = Mathf.Max(0.01f, m_source.minDistance);
        float maxDistance = Mathf.Max(minDistance + 0.01f, m_stopDistance);

        if (distance <= minDistance)
        {
            return 1f;
        }

        return Mathf.Clamp01(1f - ((distance - minDistance) / (maxDistance - minDistance)));
    }

    private bool TryResolveListener()
    {
        if (string.IsNullOrWhiteSpace(m_listenerTag))
        {
            return false;
        }

        var listenerObject = GameObject.FindGameObjectWithTag(m_listenerTag);
        if (listenerObject == null)
        {
            return false;
        }

        m_listener = listenerObject.transform;
        return true;
    }

    private static void CopySourceSettings(AudioSource from, AudioSource to)
    {
        if (from == null || to == null)
        {
            return;
        }

        to.playOnAwake = false;
        to.loop = false;
        to.outputAudioMixerGroup = from.outputAudioMixerGroup;
        to.mute = from.mute;
        to.bypassEffects = from.bypassEffects;
        to.bypassListenerEffects = from.bypassListenerEffects;
        to.bypassReverbZones = from.bypassReverbZones;
        to.priority = from.priority;
        to.pitch = 1f;
        to.panStereo = from.panStereo;
        to.spatialBlend = from.spatialBlend;
        to.reverbZoneMix = from.reverbZoneMix;
        to.dopplerLevel = from.dopplerLevel;
        to.spread = from.spread;
        to.rolloffMode = from.rolloffMode;
        to.minDistance = from.minDistance;
        to.maxDistance = from.maxDistance;
        to.volume = 1f;
    }
}
