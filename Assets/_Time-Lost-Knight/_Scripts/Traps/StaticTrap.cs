using UnityEngine;

public class StaticTrap : Trap
{
    [SerializeReferenceDropdown]
    [SerializeReference] public IEffect[] effects;

    protected virtual void OnTriggerEnter2D(Collider2D collision) => 
        ApplyEffects(collision);

    protected override void ApplyEffects(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Core>(out var core))
        {
            return;
        }

        effects.ApplyEffect(core.effectables);
    }
}