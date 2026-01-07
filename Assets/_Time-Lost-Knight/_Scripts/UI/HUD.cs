using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private WalletView m_walletView;
    [SerializeField] private Player m_plyaer;

    private void Start()
    {
        m_walletView.Initialize(m_plyaer.context.wallet);
    }
}