using UnityEngine;

public sealed class SpawnerPlayer : MonoBehaviour 
{
    [SerializeField] private PlayerController m_player;
    [SerializeField] private PlayerData m_playerData;

    [SerializeField] private Transform m_spawnPoint;

    public void Spawn()
    {
        var player = Object.Instantiate(m_player, m_spawnPoint.position, m_spawnPoint.rotation);

        player.Initialize(m_playerData);
    } 
}