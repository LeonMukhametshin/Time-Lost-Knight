using UnityEngine;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawner m_enemySpawner;    

    public override void InstallBindings()
    {
        Container
            .Bind<EnemySpawner>()
            .FromComponentInNewPrefab(m_enemySpawner)
            .AsSingle()
            .NonLazy();
    }
}