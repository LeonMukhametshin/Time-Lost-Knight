using System.Collections.Generic;

public class AbilitiesContainer 
{
    private Dictionary<AbilityKey, IPlayerAbility> abilities = new();

    public void RegisterAbility(IPlayerAbility ability, AbilityKey key)
    {
        abilities[key] = ability;
        if (ability.isEnabledByDefault)
        {
            SetAbilityState(key, true);
        }
    }

    public void SetAbilityState(AbilityKey key, bool state)
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

    public IPlayerAbility GetAbility(AbilityKey key)
    {
        abilities.TryGetValue(key, out IPlayerAbility playerAbility);
        return playerAbility;
    }
}