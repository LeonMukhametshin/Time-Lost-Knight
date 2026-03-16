using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StateAudioPlayer : MonoBehaviour
{
    [SerializeField] private Entity m_entity;
    [SerializeField] private StateAudioMapping m_mapping;
    [SerializeField] private bool m_stopIfNotMapped = true;

    [SerializeField] private AudioSource m_source;

    private string m_lastKey;

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
    }

    private void Start()
    {
        if (m_entity is Player && m_mapping != null && m_mapping.TryGet("PlayerIdleState", out var clip))
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
            if (key != m_lastKey)
            {
                m_lastKey = key;
                PlayClip(clip);
            }
            else if (clip.loop && (m_source.clip != clip.clip || !m_source.isPlaying))
            {
                PlayClip(clip);
            }
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
}
