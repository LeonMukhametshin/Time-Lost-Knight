using System.Collections.Generic;

public interface IAbilitiesContainer
{
    int ActiveAbilitiesCount { get; }

    void RegisterAbility(IPlayerAbility ability, string key);
    IPlayerAbility GetAbility(string key);
    void ActivateAbility(string key);
    void DeactivateAbility(string key);
    void UpdateAllAbilities();
    void UpdateAllAbilitiesFixed();
    List<IPlayerAbility> GetAllActiveAbilities();
}