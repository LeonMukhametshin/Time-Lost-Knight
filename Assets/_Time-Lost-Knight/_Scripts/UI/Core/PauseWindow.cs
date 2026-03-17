using Game.Core.ServiceLocatorSpace;
using Game.UI;
using Game.UI.Input;
using Game.UI.PopupWindow;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.UI.CoreSystem
{
    [MovedFrom("")]
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private Popup m_popup;

        private UIInputHandler m_inputHandler;

        public void Initialize(UIInputHandler input)
        {
            m_inputHandler = input;
            m_inputHandler.pausePressed += OpenOrClose;
        }

        public void UnSubscribe()
        {
            m_inputHandler.pausePressed -= OpenOrClose;
        }

        public void ClosePause()
        {
            m_popup.Hide(() => m_popup.gameObject.SetActive(false));

            // TODO remove to pause state (SRP)
            ServiceLocator.Get<Pause>().SetPause(false);
        }

        private void OpenOrClose()
        {
            if (m_popup.open)
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

            // TODO remove to pause state (SRP)
            ServiceLocator.Get<Pause>().SetPause(true);
        }
    }
}
