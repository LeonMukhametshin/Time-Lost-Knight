using Game.Core.ServiceLocatorSpace;
using Game.Player.FSM;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    protected PlayerFSM m_playerFSM { get; private set; }
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_unlockClip;
    [SerializeField][Range(0f, 1f)] private float m_unlockVolume = 1f;

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
}
