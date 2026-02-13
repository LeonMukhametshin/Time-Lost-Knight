using UnityEngine;

public class PlayerLedgeClibmState : PlayerState
{
    private bool m_isHanding;
    private bool m_isClimbing;

    private Vector2 m_detectedPosition;
    private Vector2 m_cornerPosition;
    private Vector2 m_startPosition;
    private Vector2 m_stopPosition;

    private int m_xInput;
    private int m_yInput;
    private bool m_jumpInput;
    private bool m_isTouchingCeiling;

    public PlayerLedgeClibmState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.movement.SetVelocityZero();

        player.transform.position = m_detectedPosition;
        m_cornerPosition = player.collisionDetector.DetermineCornerPosition();

        m_startPosition.Set(m_cornerPosition.x - (player.collisionDetector.facingDirection * data.startOffset.x),
            m_cornerPosition.y - data.startOffset.y);
        m_stopPosition.Set(m_cornerPosition.x + (player.collisionDetector.facingDirection * data.startOffset.y),
            m_cornerPosition.y + data.startOffset.y);

        player.transform.position = m_startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        m_isHanding = false;
        
        if(m_isClimbing)
        {
            player.transform.position = m_stopPosition;
            m_isClimbing = false;
        }
    }

    public override void Update()
    {
        base.Update();

        if (isAnimationFinished)
        {
            if (m_isTouchingCeiling)
            {
                fsm.SetState(player.statesContainer.crouchIdleState);
            }
            else
            {
                fsm.SetState(player.statesContainer.idleState);
            }
        }
        else
        {
            m_xInput = player.inputHandler.normalizedInputX;
            m_yInput = player.inputHandler.normalizedInputY;
            m_jumpInput = player.inputHandler.jumpInput;

            player.movement.SetVelocityZero();
            player.transform.position = m_startPosition;

            if (m_xInput == player.collisionDetector.facingDirection && m_isHanding && !m_isClimbing)
            {
                m_isTouchingCeiling = player.collisionDetector.CheckForSpace(m_cornerPosition);
                player.animationController.animator.SetBool(PlayerAnimationConst.IS_TOUCHING_CEILING, m_isTouchingCeiling);
                player.animationController.animator.SetBool(PlayerAnimationConst.LEDGE_CLIMB, true);
                m_isClimbing = true;
            }
            else if (m_yInput == -1 && m_isHanding && !m_isClimbing)
            {
                fsm.SetState(player.statesContainer.airState);
            }
            else if (m_jumpInput && !m_isClimbing)
            {
                player.statesContainer.wallJumpState.DetermineWallJumpDirection(true);
                fsm.SetState(player.statesContainer.wallJumpState);
            }
        }
    }

    public override void AnimationFinishTriger()
    {
        base.AnimationFinishTriger();
        player.animationController.animator.SetBool(PlayerAnimationConst.LEDGE_CLIMB, false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        m_isHanding = true;
    }

    public void SetDetectedPosition(Vector2 position) =>
        m_detectedPosition = position;
}