using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private EnemyViewModel[] m_enemies;
    [SerializeField] private Transform[] m_spawnPoints;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach(var poin in m_spawnPoints)
        {
            var enemy = GetEnemy();
            var enemyInstance = GameObject.Instantiate(enemy, poin.position, poin.rotation);
            enemyInstance.Initialize();
        }
    }

    private EnemyViewModel GetEnemy() =>
        m_enemies[Random.Range(0, m_enemies.Length)];
}