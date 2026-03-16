using Game.Buffs.Interfaces;
using Game.Core.CoreComponents;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Traps
{
    [MovedFrom("")]
    public class StaticTrap : Trap
    {
        [SerializeReferenceDropdown]
        [SerializeReference] public IEffect[] effects;

        protected virtual void OnTriggerEnter2D(Collider2D collision) =>
            ApplyEffects(collision);

        protected override void ApplyEffects(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<CoreSystem>(out var core))
            {
                return;
            }

            effects.ApplyEffect(core.effectables);
        }
    }
}
