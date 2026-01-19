using System.Collections.Generic;
using UnityEngine;

public interface ISpellProjectile
{
    public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects);
}