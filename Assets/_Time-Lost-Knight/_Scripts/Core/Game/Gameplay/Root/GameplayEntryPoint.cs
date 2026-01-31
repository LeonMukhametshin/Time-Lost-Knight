using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayEntryPoint : MonoBehaviour
{
    public event Action GoToMainMenuSceneRequested;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRootPrefab;

    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private PlayerData m_playerData;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(m_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToGameplayButtonClicked += () =>
        {
            GoToMainMenuSceneRequested?.Invoke();
        };

        LoadLevel();
    }

    private void LoadLevel()
    {
        //TODO random level selection
        SceneManager.LoadSceneAsync(
            SceneNames.LEVEL_EXAMPLE,
            LoadSceneMode.Additive)
            .completed += _ =>
            {
                SpawnAndInitializePlayer();
            };
    }

    private void SpawnAndInitializePlayer()
    {
        var spawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();

        if(spawnPoint is null)
        {
            throw new Exception("PlayerSpawnPoint not found");
        }

        var spawner = new PlayerSpawner(m_playerPrefab);
        m_playerPrefab = spawner.Spawn(spawnPoint.transform);

        var controller = m_playerPrefab.GetComponent<PlayerController>();
        controller.Initialize(m_playerData);
    }
}