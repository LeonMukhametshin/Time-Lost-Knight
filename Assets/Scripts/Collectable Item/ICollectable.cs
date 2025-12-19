using System;

public interface ICollectable
{
    event Action ñollected;
    event Action<int> ñollectedValue;
    bool TryCollect();
}