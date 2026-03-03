using UnityEngine;

public class StaticTrap : Trap
{
    [SerializeReferenceDropdown]
    [SerializeReference] public IEffect[] effects;

    public void OnTriggerEnter2D(Collider2D collision) => 
        ApplyEffects(collision);

    public override void ApplyEffects(Collider2D collision)
    {
        if (collision.TryGetComponent<IEffectable>(out var effectable))
        {
            effects.ApplyEffect(effectable);
        }
    }
}