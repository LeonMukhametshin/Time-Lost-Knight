using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private WalletView m_walletView;

    private void Start()
    {
        m_walletView.Initialize(Player.instance.context.wallet);
    }
}