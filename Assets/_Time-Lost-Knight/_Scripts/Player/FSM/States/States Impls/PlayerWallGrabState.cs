using UnityEngine;

public class PlayerWallGrabState : PlayerWallTouchingState
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    private Movement m_movement;

    private Vector2 m_holdPosition;

    public PlayerWallGrabState(Player player, EntityFSM fsm,
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

        movement.SetVelocityZero();
    }
}