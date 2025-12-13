public interface IPlayerAbility
{
    string key { get; }

    bool isActive { get; }
    bool canExecute { get; }
    bool isEnabledByDefault { get; }

    void Update();
    void Deactivate();
    void Activate();
}