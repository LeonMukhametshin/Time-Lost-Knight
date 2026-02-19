using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform spawnPoint;

    public override void InstallBindings()
    {
        var playerInstance =
            Container.InstantiatePrefabForComponent<Player>(
                playerPrefab, spawnPoint.position, Quaternion.identity, null);

        Container
            .Bind<Player>()
            .FromInstance(playerInstance)
            .AsSingle()
            .NonLazy();
    }
}