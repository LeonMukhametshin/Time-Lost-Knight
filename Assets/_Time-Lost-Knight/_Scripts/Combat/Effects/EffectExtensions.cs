using System.Collections.Generic;

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