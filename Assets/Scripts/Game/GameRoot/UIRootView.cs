using UnityEngine;

public class UIRootView : MonoBehaviour 
{
    [SerializeField] private GameObject m_loadingScreen;

    public void Awake()
    {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen() =>
        m_loadingScreen.SetActive(true);

    public void HideLoadingScreen() =>
        m_loadingScreen.SetActive(false);
}