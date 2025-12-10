public class WalkAbility : BasePlayerAbility
{
    private WalkAbilityConfig m_walkConfig;

    public void Initialize(ICharacterMovement movement, IPlayerInput input, WalkAbilityConfig config)
    {
        base.Initialize(movement, input);
        m_walkConfig = config;
    }

    public override void HandleInput()
    {
        if (!m_isActive) return;

        float moveDirection = m_input.GetHorizontalInput();
        if (UnityEngine.Mathf.Abs(moveDirection) > 0.01f)
        {
            float desiredSpeed = moveDirection * m_movement.MaxHorizontalSpeed * m_walkConfig.WalkSpeedMultiplier;
            m_movement.SetHorizontalVelocity(desiredSpeed);
        }
    }
}