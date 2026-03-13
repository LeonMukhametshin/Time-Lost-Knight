using UnityEngine;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private Popup m_popup;

    public void Initialize(Player player)
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