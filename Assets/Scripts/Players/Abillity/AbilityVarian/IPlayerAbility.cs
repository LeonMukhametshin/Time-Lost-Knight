public interface IPlayerAbility
{
    string key { get; }

    bool isActive { get; }
    bool isEnabledByDefault { get; }

    void Update();
    void Deactivate();
    void Activate();
}