using UnityEngine;

public class PlayerDashState : PlayerAbilytiState
{
    public bool canDash { get; private set; }
    
    private bool m_isHolding;
    private bool m_dashInputStop;

    private float m_lastDashTime;

    private Vector2 m_dashDirection;
    private Vector2 m_dashDirectionInput;
    private Vector2 m_lastAfterImagePosition;

    public PlayerDashState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Enter()
    {
        base.Enter();

        canDash = false;
        player.inputHandler.UseDashInput();

        m_isHolding = true;
        m_dashDirection = Vector2.right * flipController.facingDirection;

        Time.timeScale = data.holdTimeScale;
        startTime = Time.unscaledTime;

        player.dashVizualizer.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if(movement.currentVelocity.y > 0)
        {
            movement.SetVelocityX(movement.currentVelocity.y * data.dashEndYMultiplier);
        }
    }

    public override void Update()
    {
        base.Update();

        if (isExitingState)
        {
            return;
        }

        player.animationController.animator
            .SetFloat(PlayerAnimationConstants.Y_VELOCITY, movement.currentVelocity.y);
        player.animationController.animator
            .SetFloat(PlayerAnimationConstants.X_VELOCITY, movement.currentVelocity.x);

        if (m_isHolding)
        {
            m_dashDirectionInput = player.inputHandler.dashDirectionInput;
            m_dashInputStop = player.inputHandler.dashInputStop;

            if (m_dashDirectionInput != Vector2.zero)
            {
                m_dashDirection = m_dashDirectionInput;
                m_dashDirection.Normalize();
            }

            float angle = Vector2.SignedAngle(Vector2.right, m_dashDirection);
            player.dashVizualizer.SetRotation(angle); 

            if (m_dashInputStop || Time.unscaledTime >= startTime + data.maxHoldTime)
            {
                m_isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;

                flipController.CheckIfShoudFlip(Mathf.RoundToInt(m_dashDirection.x));
                movement.SetDrag(data.drag);
                movement.SetVelocity(data.dashVelocity, m_dashDirection);
                player.dashVizualizer.SetActive(false);

                PlaceAfterImage();
            }
        }
        else
        {
            movement.SetVelocity(data.dashVelocity, m_dashDirection);
            CheckIfShoudPlaceAfterImage();

            if (Time.time >= startTime + data.dashTime)
            {
                movement.SetDrag(0f);
                isAbilityDone = true;
                m_lastDashTime = Time.time;
            }
        }
    }

    private void PlaceAfterImage()
    {
        //TODO: custom pool for images and other
        m_lastAfterImagePosition = player.transform.position;
    }

    private void CheckIfShoudPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, m_lastAfterImagePosition) 
            >= data.distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    public bool CheckIfCanDash() =>
        canDash && Time.time >= (m_lastDashTime + data.dashCooldown);

    public void ResetCanDash() =>
        canDash = true;
}