using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StateAudioPlayer : MonoBehaviour
{
    [SerializeField] private Entity m_entity;
    [SerializeField] private StateAudioMapping m_mapping;
    [SerializeField] private bool m_stopIfNotMapped = true;

    [SerializeField] private AudioSource m_source;

    private string m_lastKey;

    private void Update()
    {
        var state = m_entity?.fsm?.currentState;
        var key = state?.GetType().Name;

        if (string.IsNullOrEmpty(key) || key == m_lastKey)
        {
            return;
        }

        m_lastKey = key;

        if (m_mapping != null && m_mapping.TryGet(key, out var clip))
        {
            PlayClip(clip);
        }
        else if (m_stopIfNotMapped)
        {
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