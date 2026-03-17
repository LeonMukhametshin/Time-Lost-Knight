using Game.Buffs;
using Game.Buffs.Interfaces;
using Game.Core.ServiceLocatorSpace;
using UnityEngine;

public class FirstAidKit : MonoBehaviour 
{
    [SerializeField] private Game.Buffs.Interfaces.BuffEffect[] buff;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_healClip;
    [SerializeField][Range(0f, 1f)] private float m_healVolume = 1f;

    private BuffContainer health;

    private void Start()
    {
        health = ServiceLocator
            .Get<Game.Player.IPlayerFactory>().Create()
            .GetComponent<BuffContainer>();
    }

    public void Heal()
    {
        buff.ApplyEffect(health);
        DetachedAudioPlayer.Play(m_audioSource, m_healClip, m_healVolume);
        Destroy(gameObject);    
    } 
}
