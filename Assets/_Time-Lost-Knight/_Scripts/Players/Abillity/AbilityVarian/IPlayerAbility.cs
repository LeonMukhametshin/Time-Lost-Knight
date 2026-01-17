public interface IPlayerAbility
{
    bool isEnabledByDefault { get; }

    void Do(AbilityContext contex);
    void Deactivate();
    void Activate();
}