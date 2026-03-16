using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint m_instance;

    private CoroutineRunner m_coroutines;
    private UIRootView m_uiRoot;
    private bool m_isLoading;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        Application.targetFrameRate = 60;

        m_instance = new GameEntryPoint();

        m_instance.RunGame();
    }

    private GameEntryPoint()
    {
        m_coroutines = new GameObject("[COROUTINES]").AddComponent<CoroutineRunner>();

        //TODO: adressables
        var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        m_uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(m_uiRoot.gameObject);
    }

    private void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == SceneNames.GAMEPLAY)
        {
            m_coroutines.StartCoroutine(LoadAndStartGameplay());
            return;
        }

        if(sceneName == SceneNames.MAIN_MENU)
        {
            m_coroutines.StartCoroutine(LoadAndStartMainMenu(false));
            return;
        }

        if (sceneName != SceneNames.BOOT)
        {
            return;
        }
#endif

        // App startup: show main menu without loading screen
        m_coroutines.StartCoroutine(LoadAndStartMainMenu(false));
    }

    private IEnumerator LoadAndStartGameplay()
    {
        if (m_isLoading)
        {
            yield break;
        }
        m_isLoading = true;

        if (m_uiRoot != null)
        {
            m_uiRoot.ShowLoadingScreen();
        }

        if (SceneManager.GetActiveScene().name != SceneNames.BOOT)
        {
            yield return LoadScene(SceneNames.BOOT);
        }
        yield return LoadScene(SceneNames.GAMEPLAY);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        if (sceneEntryPoint == null)
        {
            Debug.LogError("GameplayEntryPoint not found in Gameplay scene");
            if (m_uiRoot != null)
            {
                m_uiRoot.HideLoadingScreen();
            }
            m_isLoading = false;
            yield break;
        }

        void OnGameplayReady()
        {
            sceneEntryPoint.gameplayReady -= OnGameplayReady;
            if (m_uiRoot != null)
            {
                m_uiRoot.HideLoadingScreen();
            }
            m_isLoading = false;
        }

        sceneEntryPoint.gameplayReady -= OnGameplayReady;
        sceneEntryPoint.gameplayReady += OnGameplayReady;

        sceneEntryPoint.goToMainMenuSceneRequested += () =>
        {
            m_coroutines.StartCoroutine(LoadAndStartMainMenu(true));
        };

        sceneEntryPoint.Run();
    }

    private IEnumerator LoadAndStartMainMenu(bool showLoading)
    {
        if (m_isLoading)
        {
            yield break;
        }
        m_isLoading = true;

        if (showLoading && m_uiRoot != null)
        {
            m_uiRoot.ShowLoadingScreen();
        }

        if (SceneManager.GetActiveScene().name != SceneNames.BOOT)
        {
            yield return LoadScene(SceneNames.BOOT);
        }
        yield return LoadScene(SceneNames.MAIN_MENU);

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        if (sceneEntryPoint == null)
        {
            Debug.LogError("MainMenuEntryPoint not found in MainMenu scene");
            if (showLoading && m_uiRoot != null)
            {
                m_uiRoot.HideLoadingScreen();
            }
            m_isLoading = false;
            yield break;
        }

        sceneEntryPoint.Run();

        sceneEntryPoint.GoToGameplaySceneRequested += () =>
        {
            m_coroutines.StartCoroutine(LoadAndStartGameplay());
        };

        if (showLoading && m_uiRoot != null)
        {
            m_uiRoot.HideLoadingScreen();
        }
        m_isLoading = false;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
