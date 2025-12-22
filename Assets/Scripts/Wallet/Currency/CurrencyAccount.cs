using System;

public class CurrencyAccount : ICurrency
{
    public event Action<int> changeCurrency;

    private int m_amount;
    private readonly string m_currencyCode;

    public int Amount
    {
        get => m_amount;
        set
        {
            if (m_amount != value)
            {
                m_amount = value;
                changeCurrency?.Invoke(m_amount);
            }
        }
    }

    public string currencyCode => m_currencyCode;

    public CurrencyAccount(int amount, string code)
    {
        Amount = amount;
        m_currencyCode = code;
    }

    public void Add(int amout) =>
        Amount += amout;

    public void Subtract(int amount) =>
        Amount -= amount;

    public bool TryAdd(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }
        Add(amount);
        return true;
    }

    public bool TrySubstract(int amount)
    {
        if (Amount >= amount)
        {
            Subtract(amount);
            return true;
        }
        return false;
    }

    public int GetAmount() =>
        Amount;

    public string GetAccountKey() =>
        currencyCode;

    public void Clear() =>
        Amount = 0;
}