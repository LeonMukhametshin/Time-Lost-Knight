using System.Collections.Generic;
using UnityEngine;

public class AbilitiesContainer 
{
    private Dictionary<string, IPlayerAbility> abilities = new();

    public void RegisterAbility(IPlayerAbility ability, string key)
    {
        abilities[key] = ability;
        if (ability.isEnabledByDefault)
        {
            Debug.Log("Registed " + ability);
            SetAbilityState(key, true);
        }
    }

    public void SetAbilityState(string key, bool state)
    {
        if (abilities.TryGetValue(key, out var ability))
        {
            if(state)
            {
                ability.Activate();
            }
            else
            {
                ability.Deactivate();
            }
        }
    }

    public IPlayerAbility GetAbility(string key)
    {
        Debug.Log(key);
        abilities.TryGetValue(key, out IPlayerAbility playerAbility);
        return playerAbility;
    }
}