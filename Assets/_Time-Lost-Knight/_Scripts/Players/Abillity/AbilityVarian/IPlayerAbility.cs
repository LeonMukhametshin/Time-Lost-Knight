public interface IPlayerAbility
{
    bool isActive { get; }
    bool isEnabledByDefault { get; }

    void Do(AbilityContext contex);
    void Deactivate();
    void Activate();
}