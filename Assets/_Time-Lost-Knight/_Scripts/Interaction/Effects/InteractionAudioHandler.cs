using Game.Audio;
using Game.Observer;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.Effects
{
    [Serializable]
    [MovedFrom("")]
    public class InteractionAudioHandler : IObserver
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private InteractionAudioConfig m_audioConfig;

        public void Notify()
        {
            m_audioSource.PlayOneShot(m_audioConfig.clip, m_audioConfig.volume);
        }
    }
}
