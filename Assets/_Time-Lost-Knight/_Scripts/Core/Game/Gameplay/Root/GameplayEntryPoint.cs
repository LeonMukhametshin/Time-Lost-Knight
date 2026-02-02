using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayEntryPoint : MonoBehaviour
{
    public event Action goToMainMenuSceneRequested;
    public event Action levelLoaded;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRootPrefab;

    private void OnEnable()
    {
        levelLoaded += SpawnPlayer;
        levelLoaded += SpawnEnemy;
    }

    private void OnDisable()
    {
        levelLoaded -= SpawnPlayer;
        levelLoaded -= SpawnEnemy;
    }

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(m_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToGameplayButtonClicked += () =>
        {
            goToMainMenuSceneRequested?.Invoke();
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
                levelLoaded?.Invoke();
            };
    }

    private void SpawnPlayer()
    {
        //remove
        var spawner = FindFirstObjectByType<SpawnerPlayer>();

        if(spawner is null)
        {
            throw new Exception("PlayerSpawnPoint not found");
        }

        spawner.Spawn();
    }

    private void SpawnEnemy()
    {
        //remove
        var spawner = FindFirstObjectByType<SpawnerEnemy>();

        if(spawner is null)
        {   
            throw new Exception("SpawnerEnemy not found");   
        }

        spawner.Spawn();
    }
}