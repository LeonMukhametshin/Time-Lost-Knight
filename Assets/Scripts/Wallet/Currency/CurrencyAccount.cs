using System;

public class CurrencyAccount : ICurrency
{
    public event Action changeCurrency;

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
                changeCurrency?.Invoke();
            }
        }
    }

    public string currencyCode => m_currencyCode;

    public CurrencyAccount(int amount, string code)
    {
        Amount = amount;
        m_currencyCode = code;
    }

    public void Add(int amount) =>
        Amount += amount;

    public void Clear() =>
        Amount = 0;

    public int Get() =>
        Amount;

    public void Subtract(int amount) =>
        Amount -= amount;

    public bool TrySubstract(int amount)
    {
        if (Amount >= amount)
        {
            Amount -= amount;
            return true;
        }
        return false;
    }
}