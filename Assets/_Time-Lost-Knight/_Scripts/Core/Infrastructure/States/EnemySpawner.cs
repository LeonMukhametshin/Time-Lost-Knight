using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Entity[] m_enemies;
    [SerializeField] private Transform[] m_spawnPoints;

    [Inject] private DiContainer m_container;

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        foreach(var spawnPoint in m_spawnPoints)
        {
            var enemy = GetEntity;
            m_container.InstantiatePrefabForComponent<Entity>(
                enemy,
                spawnPoint.position,
                Quaternion.identity,
                spawnPoint);
        }
    }

    private Entity GetEntity =>
        m_enemies[Random.Range(0, m_enemies.Length)];
}