using UnityEngine;

public class WalkAbility : IPlayerAbility
{
    public string key => "Walk";
    public bool isActive => m_isActive;
    public bool isEnabledByDefault => true;

    private bool m_isActive = true;

    private readonly PlayerMovementController m_movement;
    private readonly PlayerData m_data;

    private Vector2 m_moveInput;

    public WalkAbility(
        PlayerMovementController movement,
        PlayerData data)
    {
        m_movement = movement;
        m_data = data;
    }

    public void Activate()
    {
        m_isActive = true;
    }

    public void Deactivate()
    {
        m_isActive = false;
        m_moveInput = Vector2.zero;
    }

    public void DoWalk(Vector2 input)
    {
        if (!m_isActive) return;

        m_moveInput = input;

        m_movement.UpdateFacing(input.x);
    }

    public void Update()
    {
        if (!m_isActive) return;
        if (m_movement.state.IsDashing) return;

        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Rigidbody2D rb = m_movement.rigidbody;

        float targetSpeed = m_moveInput.x * m_data.RunMaxSpeed;
        float currentSpeed = rb.linearVelocity.x;

        bool hasInput = Mathf.Abs(m_moveInput.x) > 0.01f;

        float accel = hasInput
            ? m_data.RunAcceleration
            : m_data.RunDeceleration;

        if (m_movement.state.lastOnGroundTime <= 0)
        {
            accel *= m_data.AirAccelMultiplier;
        }

        float newSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            accel * Time.deltaTime
        );

        rb.linearVelocity = new Vector2(newSpeed, rb.linearVelocity.y);
    }
}