using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Entity[] m_enemies;
    [SerializeField] private Transform[] m_spawnPoints;

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        foreach(var spawnPoint in m_spawnPoints)
        {
            var enemy = GetEntity;
            GameObject.Instantiate(enemy, spawnPoint.transform);
        }
    }

    private Entity GetEntity =>
        m_enemies[Random.Range(0, m_enemies.Length)];
}