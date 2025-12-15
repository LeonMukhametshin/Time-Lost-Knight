using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsWalletSaver : IWalletSaver
{
    public const string WALLET_KEY = "wallet";

    public bool Load(out Dictionary<string, CurrencyAccount> wallet)
    {
        wallet = new Dictionary<string, CurrencyAccount>();
        var data = PlayerPrefs.GetString(WALLET_KEY, "");

        if (string.IsNullOrEmpty(data))
        {
            return false;
        }

        wallet = Serializer.Deserialize<Dictionary<string, CurrencyAccount>>(Convert.FromBase64String(data));
        return true;
    }

    public bool Save(Dictionary<string, CurrencyAccount> wallet)
    {
        PlayerPrefs.SetString(WALLET_KEY, Convert.ToBase64String(Serializer.Serialize(wallet)));
        return true;
    }
}