using UnityEngine;

public sealed class SpawnerEnemy : MonoBehaviour
{
    [SerializeReference] private EnemyData[] m_enemyDatas;
    [SerializeField] private EnemyModel[] m_enemies;
    [SerializeField] private Transform[] m_spawnPoints;

    private bool isSpawning;

    public void Spawn()
    {
        if(isSpawning)
        {
            return;
        }

        foreach(var poin in m_spawnPoints)
        {
            var enemy = GetEnemy();
            var enemyInstance = GameObject.Instantiate(enemy, poin.position, poin.rotation);
            enemyInstance.Initialize(GetEnemyData());
        }

        isSpawning = true;
    }

    private EnemyModel GetEnemy() =>
        m_enemies[Random.Range(0, m_enemies.Length)];

    private EnemyData GetEnemyData() =>
        m_enemyDatas[Random.Range(0, m_enemyDatas.Length)];
}