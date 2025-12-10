public interface IPlayerAbility
{
    bool IsActive { get; }
    bool CanExecute { get; }

    void OnEnable();
    void OnDisable();
    void Initialize(ICharacterMovement movement, IPlayerInput input);
    void HandleInput();
    void FixedUpdate();
    void Update();
}