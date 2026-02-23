using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private Popup m_popup;

    private UIInputHandler m_inputHandler;

    public void Initialize()
    {
        m_inputHandler = ServiceLocator.Get<UIInputHandler>();

        m_inputHandler.pausePressed += OpenOrClose;
    }

    public void UnSubscribe()
    {
        m_inputHandler.pausePressed -= OpenOrClose;
    }

    private void OpenOrClose()
    {
        if(m_popup.open)
        {
            ClosePause();
        }
        else
        {
            OpenPause();
        }
    }

    private void OpenPause()
    {
        m_popup.gameObject.SetActive(true);
        m_popup.Show();
        Pause.instants.SetPause(true);
    }

    private void ClosePause()
    {
        m_popup.Hide(() => m_popup.gameObject.SetActive(false));
        Pause.instants.SetPause(false);
    }
}