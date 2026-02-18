using UnityEngine;

public class PlayerDropDownState : PlayerState
{
    private float m_duration;


    public PlayerDropDownState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
        m_duration = playerData.dropThroughDuration;
    }

    public override void Enter()
    {
        base.Enter();

        player.oneWayPlatformCollisionController.SetIgnorePlatform();
        player.movement.SetVelocityY(-data.dropVelocity);

        startTime = Time.time;
        m_duration = data.dropThroughDuration;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= startTime + m_duration)
        {
            var inAirState = player.statesContainer.GetState<PlayerInAirState>();
            inAirState.StartCoyoteTime();
            fsm.SetState(inAirState);
        }
    }
}