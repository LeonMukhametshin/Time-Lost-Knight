using System.Collections.Generic;
using UnityEngine;

public interface ISpellMelee
{
    public void Initialize(Vector3 attackPosition, Vector2 radius, IReadOnlyCollection<IEffect> effects);
}