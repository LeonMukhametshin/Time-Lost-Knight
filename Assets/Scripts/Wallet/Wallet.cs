using System.Collections.Generic;

public class Wallet
{
    public IWalletSaver saveLoad;

    private Dictionary<string, CurrencyAccount> m_wallet = new Dictionary<string, CurrencyAccount>();

    public Wallet(IWalletSaver walletSaver, Dictionary<string, CurrencyAccount> wallet = null)
    {
        this.saveLoad = walletSaver;
        if(wallet is null)
        {
            Load();
        }
    }

    public bool Load()
    {
        if (saveLoad.Load(out var newWallet))
        {
            m_wallet = newWallet;
            return true;
        }
        return false;
    }

    public bool Save() =>
        saveLoad.Save(m_wallet);

    public bool AddAccount(CurrencyAccount newAccount)
    {
        if (m_wallet.ContainsKey(newAccount.currencyCode))
        {
            return false;
        }
        m_wallet.Add(newAccount.currencyCode, newAccount);
        return true;
    }
}