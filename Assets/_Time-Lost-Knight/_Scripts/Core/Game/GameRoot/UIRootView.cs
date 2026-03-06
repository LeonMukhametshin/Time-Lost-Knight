using TMPro;
using UnityEngine;

public class UIRootView : MonoBehaviour 
{
    [SerializeField] private GameObject m_loadingScreen;
    [SerializeField] private TMP_Text m_loadingText;
    [SerializeField] private Canvas m_loadingScreenCanvas;
    [SerializeField] private int m_loadingScreenSortingOrder = 1000;

    [SerializeField] private string[] m_phrases;

    public void Awake()
    {
        EnsureLoadingScreenOnTop();
        HideLoadingScreen();
    }

    public void ShowLoadingScreen()
    {
        EnsureLoadingScreenOnTop();
        m_loadingScreen.SetActive(true);
        m_loadingText.text = GetRandomPhrase();
    }
       
    public void HideLoadingScreen() =>
        m_loadingScreen.SetActive(false);

    private void EnsureLoadingScreenOnTop()
    {
        m_loadingScreen.transform.SetAsLastSibling();

        if (m_loadingScreenCanvas != null)
        {
            m_loadingScreenCanvas.overrideSorting = true;
            m_loadingScreenCanvas.sortingOrder = m_loadingScreenSortingOrder;
        }
    }

    private string GetRandomPhrase()
    {
        var index = Random.Range(0, m_phrases.Length);
        var phrase = m_phrases[index];

        return phrase;
    }
}