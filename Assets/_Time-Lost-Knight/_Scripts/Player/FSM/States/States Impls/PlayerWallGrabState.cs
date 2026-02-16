using UnityEngine;

public class PlayerWallGrabState : PlayerWallTouchingState
{
    private Vector2 m_holdPosition;

    public PlayerWallGrabState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName) 
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

        HoldPosition();

        if (!isExitingState)
        {
            return;   
        }

        if (yInput > 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallClimbState>());
        }
        else if (yInput < 0 || !grabInput)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallSlideState>());
        }
    }

    private void HoldPosition()
    {
        player.transform.position = m_holdPosition;

        core.movement.SetVelocityZero();
    }
}