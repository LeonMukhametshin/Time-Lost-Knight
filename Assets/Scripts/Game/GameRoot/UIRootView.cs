using UnityEngine;

public class UIRootView : MonoBehaviour 
{
    [SerializeField] private GameObject m_loadingScreen;
    [SerializeField] private Transform m_uiSceneContainer;

    public void Awake()
    {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen() =>
        m_loadingScreen.SetActive(true);

    public void HideLoadingScreen() =>
        m_loadingScreen.SetActive(false);

    public void AttachSceneUI(GameObject sceneUI)
    {
        ClearSceneUI();

        sceneUI.transform.SetParent(m_uiSceneContainer, false);
    }

    private void ClearSceneUI()
    {
        var childCount = m_uiSceneContainer.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(m_uiSceneContainer.GetChild(i));
        }
    }
}