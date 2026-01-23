using UnityEngine;

public class WalkAbility : IPlayerAbility
{
    public bool isEnabledByDefault => m_isEnabled;
    public bool isActive => m_isActive;

    private PlayerMoveData m_moveData;
    private Rigidbody2D m_rigidbody2D;

    private bool m_isEnabled = true;
    private bool m_isActive = false;

    public WalkAbility(PlayerMoveData data, Rigidbody2D rigidbody2D)
    {
        m_moveData = data;
        m_rigidbody2D = rigidbody2D;
    }

    public void Activate()
    {
        m_isEnabled = true;
    }

    public void Deactivate()
    {
        m_isEnabled = false;
    }

    public void Do(AbilityContext contex)
    {
        if(!m_isEnabled)
        {
            return;
        }

        ApplyMovement(contex.moveDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        float targetSpeed = direction.x * m_moveData.runMaxSpeed;
        float currentSpeed = m_rigidbody2D.linearVelocity.x;

        bool hasInput = Mathf.Abs(direction.x) > 0.01f;

        float accel = hasInput
            ? m_moveData.runAcceleration
            : m_moveData.runDeceleration;

        float newSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            accel 
        );

        m_rigidbody2D.linearVelocity = new Vector2(newSpeed, m_rigidbody2D.linearVelocity.y);
    }
}