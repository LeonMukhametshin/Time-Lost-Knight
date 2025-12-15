using System;

public interface ICurrency
{
    string currencyCode { get; }

    event Action<int> changeCurrency;
    int Get();
    string GetAccount();
    void Add(int amount);
    void Subtract(int amount);
    bool TrySubstract(int amount);
    void Clear();
}