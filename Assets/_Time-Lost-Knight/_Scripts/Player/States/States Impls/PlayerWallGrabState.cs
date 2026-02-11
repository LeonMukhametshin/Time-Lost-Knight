using UnityEngine;

public class PlayerWallGrabState : PlayerWallTouchingState
{
    private Vector2 m_holdPosition;

    public PlayerWallGrabState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        m_holdPosition = player.transform.position;
        HoldPosition();
    }

    public override void Update()
    {
        base.Update();

        if(!isExitingState)
        {
            return;   
        }

        HoldPosition();

        if (yInput > 0)
        {
            fsm.SetState(player.statesContainer.wallClimbState);
        }
        else if (yInput < 0 || !grabInput)
        {
            fsm.SetState(player.statesContainer.wallSlideState);
        }
    }

    private void HoldPosition()
    {
        player.transform.position = m_holdPosition;

        player.movement.SetVelocityZero();
    }
}