using System;

public interface ICurrency
{
    event Action OnChange;
    int Get();
    void Add(int amount);
    void Subtract(int amount);
    bool TrySubstract(int amount);
    void Clear();
}