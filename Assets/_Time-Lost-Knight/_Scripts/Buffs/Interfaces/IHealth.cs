using System;

public interface IHealth
{
    event Action died;
    event Action valueChanged;

    float maxValue { get; }

    float value { get; }

    void TakeDamage(float damage);
    void Heal(float heal);
}