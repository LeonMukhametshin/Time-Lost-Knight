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

        m_playerMovement.m_rigidbody.linearVelocity = new Vector2(velocityX, m_playerMovement.m_rigidbody.linearVelocityY);
    }

    private float CalculateVelocity()
    {
        float targetSpeed = m_playerMovement.MoveInput.x * m_playerMovement.Data.RunMaxSpeed;

        float speed = Mathf.MoveTowards(
            m_playerMovement.m_rigidbody.linearVelocityX,
            targetSpeed,
            (m_playerMovement.LastOnGroundTime > 0
                ? m_playerMovement.Data.RunAcceleration
                : m_playerMovement.Data.RunAcceleration * m_playerMovement.Data.AirAccelMultiplier) * Time.fixedDeltaTime
        );

        return speed;
    }

    public void Update()
    {
       
    }
}