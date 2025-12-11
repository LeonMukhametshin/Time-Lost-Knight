public class WalkAbility : BasePlayerAbility
{
    private WalkAbilityConfig m_walkConfig;
    private float m_moveDirectionX;

    public void Initialize(ICharacterMovement movement, IPlayerInput input, WalkAbilityConfig config)
    {
        base.Initialize(movement, input);
        m_walkConfig = config;
    }

    public override void HandleInput()
    {
        if (!m_isActive) return;

        m_moveDirectionX = m_input.GetHorizontalInput();
        if (UnityEngine.Mathf.Abs(m_moveDirectionX) > 0.01f)
        {
            Flip();
            float desiredSpeed = m_moveDirectionX * m_movement.MaxHorizontalSpeed 
                * m_walkConfig.WalkSpeedMultiplier;
            m_movement.SetHorizontalVelocity(desiredSpeed);
        }
    }

    private void Flip()
    {
        if (m_moveDirectionX > 0)
        {
            transform.localScale = new UnityEngine.Vector3(1, 1,1);
        }
        else
        {
            transform.localScale = new UnityEngine.Vector3(-1, 1, 1);
        }
    }
}