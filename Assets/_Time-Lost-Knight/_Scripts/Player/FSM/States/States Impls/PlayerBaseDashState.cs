using UnityEngine;

public abstract class PlayerBaseDashState : PlayerAbilytiState
{
    public bool canDash;

    private bool m_isHolding;

    private float m_lastDashTime;
    private Vector2 m_dashDirection;
    private Vector2 m_lastAfterImagePosition;

    protected PlayerBaseDashState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active) 
        : base(fsm, core, 
            animBoolName, player, 
            data, active)
    {
    }

    protected abstract bool canHoldDirection { get; }
    protected abstract bool showDashVisualizer { get; }

    public override void Enter()
    {
        base.Enter();

        canDash = false;
        player.inputHandler.UseDashInput();

        m_dashDirection = ResolveDashDirection(Vector2.right * flipController.facingDirection);

        m_isHolding = canHoldDirection;

        if (m_isHolding)
        {
            Time.timeScale = data.holdTimeScale;
            startTime = Time.unscaledTime;
        }
        else
        {
            startTime = Time.time;
            StartDashMove();
        }

        player.dashVizualizer.SetActive(showDashVisualizer);
    }

    public override void Exit()
    {
        base.Exit();

        Time.timeScale = 1f;

        if (movement.currentVelocity.y > 0)
        {
            movement.SetVelocityY(movement.currentVelocity.y * data.dashEndYMultiplier);
        }
    }

    public override void Update()
    {
        base.Update();

        if (isExitingState)
        {
            return;
        }

        player.animator.SetFloat(PlayerAnimationConstants.Y_VELOCITY, movement.currentVelocity.y);
        player.animator.SetFloat(PlayerAnimationConstants.X_VELOCITY, movement.currentVelocity.x);

        if (m_isHolding)
        {
            m_dashDirection = ResolveDashDirection(m_dashDirection);

            float angle = Vector2.SignedAngle(Vector2.right, m_dashDirection);
            player.dashVizualizer.SetRotation(angle);

            if (player.inputHandler.dashInputStop || Time.unscaledTime >= startTime + data.maxHoldTime)
            {
                m_isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;
                StartDashMove();
                player.dashVizualizer.SetActive(false);
            }

            return;
        }

        movement.SetVelocity(data.dashVelocity, m_dashDirection);
        CheckIfShoudPlaceAfterImage();

        if (Time.time >= startTime + data.dashTime)
        {
            movement.SetDrag(0f);
            isAbilityDone = true;
            m_lastDashTime = Time.time;
        }
    }

    protected abstract Vector2 ResolveDashDirection(Vector2 fallbackDirection);

    public bool CheckIfCanDash() =>
        canDash && Time.time >= (m_lastDashTime + data.dashCooldown);

    public void ResetCanDash() =>
        canDash = true;

    private void StartDashMove()
    {
        flipController.CheckIfShoudFlip(Mathf.RoundToInt(m_dashDirection.x));
        movement.SetDrag(data.drag);
        movement.SetVelocity(data.dashVelocity, m_dashDirection);

        m_lastAfterImagePosition = player.transform.position;
    }

    private void CheckIfShoudPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, m_lastAfterImagePosition) >= data.distanceBetweenAfterImages)
        {
            m_lastAfterImagePosition = player.transform.position;
        }
    }
}