using TMPro;
using UnityEngine;

public class UIRootView : MonoBehaviour 
{
    [SerializeField] private GameObject m_loadingScreen;
    [SerializeField] private Transform m_uiSceneContainer;
    [SerializeField] private TMP_Text m_loadingText;

    [SerializeField] private string[] m_phrases =
    {
        "Phrases1...",
        "Phrases2...",
        "Phrases3...",
        "Phrases4...",
        "Phrases5...",
        "Phrases6...",
    };

    public void Awake()
    {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen()
    {
        m_loadingScreen.SetActive(true);
        m_loadingText.text = GetRandomPhrase();
    }
       
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
            Destroy(m_uiSceneContainer.GetChild(i).gameObject);
        }
    }

    private string GetRandomPhrase()
    {
        var index = Random.Range(0, m_phrases.Length);
        var phrase = m_phrases[index];

        return phrase;
    }
}