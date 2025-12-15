using System;

public interface ICollectable
{
    event Action collect;
    void Collect();   
}