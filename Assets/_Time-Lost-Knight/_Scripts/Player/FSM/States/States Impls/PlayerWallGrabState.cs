using UnityEngine;

public class PlayerWallGrabState : PlayerWallTouchingState
{
    private Vector2 m_holdPosition;

    public PlayerWallGrabState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool canUse_) 
        : base(fsm, core, animBoolName, player, data)
    {
        this.canUse = canUse_;
    }

    public override void Enter()
    {
        canUse = player.canGrab;

        if (!canUse)
        {
            return;
        } 

        base.Enter();

        m_holdPosition = player.transform.position;
        HoldPosition();
    }

    public override void Update()
    {
        base.Update();

        HoldPosition();

        if (isExitingState)
        {
            return;   
        }

        if (yInput > 0)
        {
            fsm.ChangeState<PlayerWallClimbState>();;
        }
        else if (yInput < 0 || !grabInput)
        {
            fsm.ChangeState<PlayerWallSlideState>();;
        }
    }

    private void HoldPosition()
    {
        player.transform.position = m_holdPosition;

        movement.SetVelocityZero();
    }
}