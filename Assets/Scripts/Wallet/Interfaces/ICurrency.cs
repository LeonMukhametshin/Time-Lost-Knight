using System;

public interface ICurrency
{
    string currencyCode { get; }

    event Action changeCurrency;
    int Get();
    void Add(int amount);
    void Subtract(int amount);
    bool TrySubstract(int amount);
    void Clear();
}