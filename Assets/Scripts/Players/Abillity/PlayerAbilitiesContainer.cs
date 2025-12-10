using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesContainer : MonoBehaviour, IAbilitiesContainer
{
    private Dictionary<string, IPlayerAbility> abilities = new Dictionary<string, IPlayerAbility>();
    private List<IPlayerAbility> activeAbilities = new List<IPlayerAbility>();

    public int ActiveAbilitiesCount => activeAbilities.Count;
    
    public void RegisterAbility(IPlayerAbility ability, string key)
    {
        abilities[key] = ability;
        if (ability.IsActive)
            activeAbilities.Add(ability);
    }

    public IPlayerAbility GetAbility(string key) => abilities.ContainsKey(key) ? abilities[key] : null;

    public void ActivateAbility(string key)
    {
        var ability = GetAbility(key);
        if (ability != null && !ability.IsActive)
        {
            ability.OnEnable();
            activeAbilities.Add(ability);
        }
    }

    public void DeactivateAbility(string key)
    {
        var ability = GetAbility(key);
        if (ability != null && ability.IsActive)
        {
            ability.OnDisable();
            activeAbilities.Remove(ability);
        }
    }

    public void UpdateAllAbilities()
    {
        foreach (var ability in activeAbilities)
        {
            ability.HandleInput();
            ability.Update();
        }
    }

    public void UpdateAllAbilitiesFixed()
    {
        foreach (var ability in activeAbilities)
            ability.FixedUpdate();
    }

    public List<IPlayerAbility> GetAllActiveAbilities() => new List<IPlayerAbility>(activeAbilities);
}