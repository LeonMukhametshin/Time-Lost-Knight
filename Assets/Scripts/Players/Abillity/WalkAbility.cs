using UnityEngine;

public class WalkAbility : IPlayerAbility
{
    private readonly PlayerMovement m_playerMovement;

    public bool isActive => m_isActive;
    public bool canExecute => true;
    public bool isEnabledByDefault => true;
    public string key => "Walk";

    private bool m_isActive = true;

    public WalkAbility(PlayerMovement playerMovement)
    {
        this.m_playerMovement = playerMovement;
    }

    public void Activate()
    {
        m_isActive = true;
    }

    public void Deactivate()
    {
        m_isActive = false;
    }

    public void DoWalk(Vector3 input)
    {
        if (!isActive || !canExecute) return;

        m_playerMovement.moveInput = input;
        m_playerMovement.Flip(input.x);
        UpdateMove();
    }

    private void UpdateMove()
    {
        if (!isActive) return;
        if (m_playerMovement.isDashing) return;

        float velocityX = CalculateVelocity();

        m_playerMovement.rigidbody.linearVelocity = 
            new Vector2(velocityX, m_playerMovement.rigidbody.linearVelocityY);
    }

    private float CalculateVelocity()
    {
        float inputX = m_playerMovement.moveInput.x;
        float targetSpeed = inputX * m_playerMovement.data.RunMaxSpeed;
        float currentSpeed = m_playerMovement.rigidbody.linearVelocityX;

        bool hasInput = Mathf.Abs(inputX) > 0.01f;

        float accel = hasInput
            ? m_playerMovement.data.RunAcceleration
            : m_playerMovement.data.RunDeceleration;

        if (m_playerMovement.lastOnGroundTime <= 0)
        {
            accel *= m_playerMovement.data.AirAccelMultiplier;
        }

        float speed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            accel * Time.fixedDeltaTime
        );

        return speed;
    }

    public void Update()
    {
       
    }
}