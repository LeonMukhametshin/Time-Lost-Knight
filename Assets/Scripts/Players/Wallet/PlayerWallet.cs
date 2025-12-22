using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static Wallet wallet;

    [SerializeField] private TextMeshProUGUI m_text;

    private void OnValidate()
    {
        if (!m_text)
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        wallet = new Wallet(new PlayerPrefsWalletSaver());
        wallet.AddAccount(new CurrencyAccount(0, "Coins"));
        wallet.GetAccount("Coins").changeCurrency += UpdateUI;
        UpdateUI(wallet.GetAccount("Coins").GetAmount());
    }

    private void UpdateUI(int amount)
    {
        m_text.text = amount.ToString();
    }
}