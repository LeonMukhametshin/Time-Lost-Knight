using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayEntryPoint : MonoBehaviour
{
    public event Action goToMainMenuSceneRequested;
    public event Action levelLoaded;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRootPrefab;

    private bool m_isInitialized;

    private CoroutineRunner m_coroutines;

    public void Initialize(CoroutineRunner coroutine)
    {
        if(m_isInitialized)
        {
            return;
        }

        m_coroutines = coroutine;
    }

    private void OnEnable()
    {
        levelLoaded += SpawnPlayer;
    }

    private void OnDisable()
    {
        levelLoaded -= SpawnPlayer;
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
        //var spawner = FindFirstObjectByType<SpawnerPlayer>();

        //if(spawner is null)
        //{
        //    throw new Exception("PlayerSpawnPoint not found");
        //}

        //spawner.Spawn(m_coroutines);
    }
}