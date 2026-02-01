using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeReference] private EnemyData[] m_enemyDatas;
    [SerializeField] private EnemyModel[] m_enemies;
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
            enemyInstance.Initialize(GetEnemyData());
        }
    }

    private EnemyModel GetEnemy() =>
        m_enemies[Random.Range(0, m_enemies.Length)];

    private EnemyData GetEnemyData() =>
        m_enemyDatas[Random.Range(0, m_enemyDatas.Length)];
}