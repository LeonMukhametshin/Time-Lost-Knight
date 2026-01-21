using System.Collections.Generic;
using UnityEngine;

public interface ISpellProjectile
{
    public void Initialize(float speed, AnimationCurve timingCurve, IReadOnlyList<IEffect> effects);
}