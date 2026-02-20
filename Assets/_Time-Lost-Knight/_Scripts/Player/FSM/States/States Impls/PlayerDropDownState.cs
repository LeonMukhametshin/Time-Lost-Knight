using UnityEngine;

public class PlayerDropDownState : PlayerState
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    protected OneWayPlatformCollisionController oneWayPlatformCollisionController
    {
        get => m_oneWayPlatformCollision ??= core.GetCoreComponent<OneWayPlatformCollisionController>();
    }

    private Movement m_movement;
    private OneWayPlatformCollisionController m_oneWayPlatformCollision;

    private float m_duration;


    public PlayerDropDownState(Player player, EntityFSM fsm,
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
        m_duration = playerData.dropThroughDuration;
    }

    public override void Enter()
    {
        base.Enter();

        oneWayPlatformCollisionController.SetIgnorePlatform();
        movement.SetVelocityY(-data.dropVelocity);

        startTime = Time.time;
        m_duration = data.dropThroughDuration;
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