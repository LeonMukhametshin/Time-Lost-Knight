using System.Collections.Generic;
using UnityEngine;

public class SpellMelee : MonoBehaviour, ISpellMelee
{
    public void Initialize(Vector3 attackPosition, Vector2 size, IReadOnlyCollection<IEffect> effects)
    {
        var colliders = Physics2D.OverlapBoxAll(attackPosition, size, gameObject.layer);
        
        foreach (var collider in colliders)
        {
            var effectable = collider.GetComponent<IEffectable>();
            effects.ApplyEffect(effectable);
        }
    }
}