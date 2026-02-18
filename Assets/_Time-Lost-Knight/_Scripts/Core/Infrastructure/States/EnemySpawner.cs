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
            var enemyInstance = GameObject.Instantiate(enemy, spawnPoint);
        }
    }

    private Entity GetEntity =>
        m_enemies[UnityEngine.Random.Range(0, m_enemies.Length)];
}