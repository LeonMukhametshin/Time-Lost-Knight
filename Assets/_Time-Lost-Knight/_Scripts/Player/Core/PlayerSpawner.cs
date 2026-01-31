using UnityEngine;

public sealed class PlayerSpawner
{
    private readonly GameObject m_playerPrefab;

    public PlayerSpawner(GameObject playerPrefab)
    {
        m_playerPrefab = playerPrefab;
    }

    public GameObject Spawn(Transform spawnPoint) =>
        Object.Instantiate(m_playerPrefab, spawnPoint.position, spawnPoint.rotation);
}