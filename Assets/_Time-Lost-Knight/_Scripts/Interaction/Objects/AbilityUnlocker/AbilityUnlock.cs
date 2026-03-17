using Game.Core.ServiceLocatorSpace;
using Game.Player.FSM;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    protected PlayerFSM m_playerFSM { get; private set; }
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_unlockClip;
    [SerializeField][Range(0f, 1f)] private float m_unlockVolume = 1f;

    private void Awake() =>
        EnsureAudioSource();

    private void Start()
    {
        m_playerFSM = ServiceLocator.Get<PlayerFSM>();
        if (m_playerFSM == null)
        {
            throw new System.Exception("PlayerFSM not found in ServiceLocator");
        }
    }


    public void PickUp() =>
        Unlock();

    protected void PlayUnlockAudio() =>
        DetachedAudioPlayer.Play(m_audioSource, m_unlockClip, m_unlockVolume);

    public virtual void Unlock() { }

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
        m_audioSource.spatialBlend = 1f;
        m_audioSource.minDistance = 2f;
        m_audioSource.maxDistance = 18f;
        m_audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
    }
}
