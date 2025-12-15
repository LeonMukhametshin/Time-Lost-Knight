using System;

public class CurrencyAccount : ICurrency
{
    public event Action OnChange;

    private int m_amount;

    public int Amount
    {
        get => m_amount;
        set
        {
            if (m_amount != value)
            {
                m_amount = value;
                OnChange?.Invoke();
            }
        }
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