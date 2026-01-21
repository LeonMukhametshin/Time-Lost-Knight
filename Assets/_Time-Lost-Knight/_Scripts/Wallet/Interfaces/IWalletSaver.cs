using System.Collections.Generic;

public interface IWalletSaver
{
    bool Save(Dictionary<string, CurrencyAccount> wallet);
    bool Load(out Dictionary<string, CurrencyAccount> wallet);
}