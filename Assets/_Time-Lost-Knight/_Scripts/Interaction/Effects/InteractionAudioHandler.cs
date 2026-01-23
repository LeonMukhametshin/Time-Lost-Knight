using System;
using UnityEngine;

[Serializable]
public class InteractionAudioHandler : IObserver
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private InteractionAudioConfig m_audioConfig;

    public void Notify()
    {
        m_audioSource.PlayOneShot(m_audioConfig.clip, m_audioConfig.volume);
    }
}