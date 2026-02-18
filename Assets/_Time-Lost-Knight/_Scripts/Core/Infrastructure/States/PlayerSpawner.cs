using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform m_spawnPoint;

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject.Instantiate(player, m_spawnPoint);
    }
}