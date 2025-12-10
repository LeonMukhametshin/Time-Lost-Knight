using UnityEngine;

public abstract class BasePlayerAbility : MonoBehaviour, IPlayerAbility
{
    protected AbilityConfig m_config;
    protected ICharacterMovement m_movement;
    protected IPlayerInput m_input;
    protected bool m_isActive;

    public virtual void Initialize(ICharacterMovement movement, IPlayerInput input)
    {
        this.m_movement = movement;
        this.m_input = input;
    }

    public virtual void HandleInput() { }
    public virtual void FixedUpdate() { }
    public virtual void Update() { }
    public virtual void OnEnable() => m_isActive = true;
    public virtual void OnDisable() => m_isActive = false;

    public bool IsActive => m_isActive;
    public virtual bool CanExecute => true;
}
