using UnityEngine;

public interface IProjectile 
{
    void Initialize(RangeAttackData data);
    Vector3 position { get; }
}