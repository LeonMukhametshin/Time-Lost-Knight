public class PlayerWallSlideState : PlayerWallTouchingState
{
    public PlayerWallSlideState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Update()
    {
        base.Update();
        movement.SetVelocityY(-UnityEngine.Mathf.Abs(data.wallSlideVelocity));

        if (isExitingState)
        {
            return;
        }

        if (grabInput && yInput == 0)
        {
            fsm.ChangeState<PlayerWallGrabState>();
        }
    }
}