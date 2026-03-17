using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Buffs.Interfaces
{
    [MovedFrom("")]
    public static class EffectExtensions
    {
        public static void ApplyEffect(
            this IReadOnlyCollection<IEffect> effects,
            IEffectable effectable)
        {
            if (effects is null) return;

            foreach (var effect in effects)
            {
                effect?.Apply(effectable);
            }
        }

        public static void ApplyEffect(
            this IReadOnlyCollection<IEffect> effects,
            IReadOnlyCollection<IEffectable> effectables)
        {
            if (effects is null) return;

            foreach (var effect in effects)
            {
                foreach (var effectable in effectables)
                {
                    effect?.Apply(effectable);
                }
            }
        }
    }
}