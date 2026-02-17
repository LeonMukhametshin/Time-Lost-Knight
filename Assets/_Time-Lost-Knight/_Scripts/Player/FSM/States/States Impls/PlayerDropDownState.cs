using UnityEngine;

public class PlayerDropDownState : PlayerState
{
    private float duration;

    public PlayerDropDownState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
        duration = playerData.dropThroughDuration;
    }

    public override void Enter()
    {
        base.Enter();

        player.oneWayPlatformCollisionController.SetIgnorePlatform();
        player.movement.SetVelocityY(-data.dropVelocity);

        startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= startTime + duration)
        {
            var inAirState = player.statesContainer.GetState<PlayerInAirState>();
            inAirState.StartCoyoteTime();
            fsm.SetState(inAirState);
        }
    }
}