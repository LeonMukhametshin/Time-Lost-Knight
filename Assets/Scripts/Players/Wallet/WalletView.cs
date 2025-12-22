using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text;

    private Wallet m_wallet;

    public void Initialize(Wallet wallet)
    {
        m_wallet = wallet;
        m_wallet.AddAccount(new CurrencyAccount(0, CurrencyType.COIN));
        m_wallet.GetAccount(CurrencyType.COIN).changeCurrency += UpdateView;
        UpdateView(m_wallet.GetAccount(CurrencyType.COIN).Amount);
    }

    private void OnDestroy()
    {
        if(m_wallet != null)
        {
            m_wallet.GetAccount(CurrencyType.COIN).changeCurrency -= UpdateView;
        }
    }

    private void UpdateView(int coins)
    {
        Debug.Log(coins);
        m_text.text = coins.ToString();
    }
}