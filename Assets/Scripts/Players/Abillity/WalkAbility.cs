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

        m_playerMovement.MoveInput = input;
        m_playerMovement.Flip(input.x);
        UpdateMove();
    }

    private void UpdateMove()
    {
        if (!isActive) return;
        if (m_playerMovement.IsDashing) return;

        float velocityX = CalculateVelocity();

        m_playerMovement.m_rigidbody.linearVelocity = 
            new Vector2(velocityX, m_playerMovement.m_rigidbody.linearVelocityY);
    }

    private float CalculateVelocity()
    {
        float inputX = m_playerMovement.MoveInput.x;
        float targetSpeed = inputX * m_playerMovement.Data.RunMaxSpeed;
        float currentSpeed = m_playerMovement.m_rigidbody.linearVelocityX;

        bool hasInput = Mathf.Abs(inputX) > 0.01f;

        float accel = hasInput
            ? m_playerMovement.Data.RunAcceleration
            : m_playerMovement.Data.RunDeceleration;

        if (m_playerMovement.LastOnGroundTime <= 0)
        {
            accel *= m_playerMovement.Data.AirAccelMultiplier;
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