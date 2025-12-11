using System.Collections;
using UnityEngine;

public class DashAbility : BasePlayerAbility
{
    private DashAbilityConfig m_dashConfig;

    private bool m_canDash = true;
    private bool m_dashed;
    private float m_gravity;

    public override bool CanExecute
    {
        get
        {
            return m_canDash && !m_dashed;
        }
    }

    public void Initialize(ICharacterMovement movement, IPlayerInput input, DashAbilityConfig config)
    {
        base.Initialize(movement, input);
        m_dashConfig = config;

        m_gravity = movement.Gravity;
    }

    public override void HandleInput()
    {
        if (m_movement.IsGrounded())
        {
            m_dashed = false;
        }

        if (!m_canDash || !m_input.IsDashPressed()) return;

        if (CanExecute)
        {
            StartCoroutine(Dash());
            m_dashed = true;
        }
    }

    private IEnumerator Dash()
    {
        m_canDash = false;
        m_movement.SetGravity(0f);
        Vector2 dashDirection = new Vector2(transform.localScale.x * m_dashConfig.DashSpeed, 0f);
        m_movement.Dash(dashDirection);
        yield return new WaitForSeconds(m_dashConfig.DashDuration);
        m_movement.SetGravity(-m_gravity);
        yield return new WaitForSeconds(m_dashConfig.DashCooldown);
        m_canDash = true;
    }
}