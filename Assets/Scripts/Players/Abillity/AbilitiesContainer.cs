using System.Collections.Generic;
using UnityEngine;

public class AbilitiesContainer : MonoBehaviour
{
    private Dictionary<string, IPlayerAbility> abilities = new();
    private List<IPlayerAbility> activeAbilities = new();

    public void RegisterAbility(IPlayerAbility ability, string key)
    {
        abilities[key] = ability;
        if (ability.isEnabledByDefault)
        {
            ActivateAbility(key);
        }
    }

    public void ActivateAbility(string key)
    {
        if (abilities.TryGetValue(key, out var ability) && !activeAbilities.Contains(ability))
        {
            ability.Activate();
            activeAbilities.Add(ability);
        }
    }

    public void DeactivateAbility(string key)
    {
        if (abilities.TryGetValue(key, out var ability) && activeAbilities.Contains(ability))
        {
            ability.Deactivate();
            activeAbilities.Remove(ability);
        }
    }

    public void UpdateAllAbilities()
    {
        for (int i = 0; i < activeAbilities.Count; i++)
        {
            activeAbilities[i].Update();
        }
    }
}