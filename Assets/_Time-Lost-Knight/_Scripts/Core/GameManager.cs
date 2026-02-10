using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_respawnPoint;
    [SerializeField] private GameObject m_player;
    [SerializeField] private Follower m_follower;
    [SerializeField] private float m_respawnTime;

    private float m_respawnTimeStart;
    private bool m_respawn;

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        m_respawnTimeStart = Time.time;
        m_respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= m_respawnTimeStart + m_respawnTime && m_respawn)
        {
            var playerInstance = Instantiate(m_player, m_respawnPoint);
            m_follower.SetTarget(playerInstance.transform);
            m_respawn = false;
        }
    }    
}   