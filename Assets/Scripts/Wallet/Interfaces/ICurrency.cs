using System;

public interface ICurrency
{
    string currencyCode { get; }

    event Action<int> changeCurrency;
    int GetAmount();
    string GetAccountKey();
    void Add(int amount);
    void Subtract(int amount);
    bool TrySubstract(int amount);
    void Clear();
}