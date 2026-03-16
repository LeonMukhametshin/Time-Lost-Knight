using Game.Core.CoreComponents;
using Game.Player;
using Game.UI.PopupWindow;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.UI.CoreSystem
{
    [MovedFrom("")]
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Popup m_popup;

        public void Initialize(PlayerController player)
        {
            player.core
                .GetCoreComponent<HealthComponent>().died += Show;

            Hide();
        }

        private void Hide()
        {
            m_popup.Hide(() => m_popup.gameObject.SetActive(false));
        }

        private void Show()
        {
            m_popup.gameObject.SetActive(true);
            m_popup.Show();
        }
    }
}
