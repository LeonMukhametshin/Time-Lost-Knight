using TMPro;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;

    private Wallet m_wallet;

    private void OnValidate()
    {
        if(!m_text)
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        m_wallet = new Wallet(new PlayerPrefsWalletSaver());
        m_wallet.AddAccount(new CurrencyAccount(0, "Coins"));
        m_wallet.GetAccount("Coins").changeCurrency += UpdateUI;
        UpdateUI(m_wallet.GetAccount("Coins").Get());
    }

    private void UpdateUI(int amount)
    {
        m_text.text = amount.ToString();
    }

    public void Add5()
    {
        m_wallet.GetAccount("Coins").Add(5);
    }

    public void Subtract7()
    {
        m_wallet.GetAccount("Coins").TrySubstract(7);
    }

    public void Clear()
    {
        m_wallet.GetAccount("Coins").Clear();
    }
}