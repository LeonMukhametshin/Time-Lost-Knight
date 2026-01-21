using System.Collections.Generic;
using UnityEngine;

public interface ISpellThrowing
{
    public void Initialize(Vector3 attackPosition, AnimationCurve direction, IReadOnlyList<IEffect> effects);
}