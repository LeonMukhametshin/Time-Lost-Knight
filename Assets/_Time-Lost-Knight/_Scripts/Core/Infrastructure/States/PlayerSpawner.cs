using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player m_player;
    [SerializeField] private Transform m_spawnPoint;

    private bool isPlayerSpawned;

    public void Spawn()
    {
        if(isPlayerSpawned)
        {
            return;
        }

        var player = GameObject.Instantiate(m_player, m_spawnPoint.position, Quaternion.identity, null);
        ServiceLocator.Register(player);

        isPlayerSpawned = true;
    }
}