using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint m_instance;

    private CoroutineRunner m_coroutines;
    private UIRootView m_uiRoot;

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
            m_coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        if (sceneName != SceneNames.BOOT)
        {
            return;
        }
#endif

        m_coroutines.StartCoroutine(LoadAndStartGameplay());
    }

    private IEnumerator LoadAndStartGameplay()
    {
        m_uiRoot.ShowLoadingScreen();

        yield return LoadScene(SceneNames.BOOT);
        yield return LoadScene(SceneNames.GAMEPLAY);

        yield return new WaitForSeconds(1f);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        sceneEntryPoint.Run(m_uiRoot);

        sceneEntryPoint.goToMainMenuSceneRequested += () =>
        {
            m_coroutines.StartCoroutine(LoadAndStartMainMenu());
        };

        m_uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        m_uiRoot.ShowLoadingScreen();

        yield return LoadScene(SceneNames.BOOT);
        yield return LoadScene(SceneNames.MAIN_MENU);

        yield return new WaitForSeconds(1f);

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(m_uiRoot);

        sceneEntryPoint.GoToGameplaySceneRequested += () =>
        {
            m_coroutines.StartCoroutine(LoadAndStartGameplay());
        };

        m_uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}