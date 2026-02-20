using UnityEngine;

public class PlayerDropDownState : PlayerState
{
    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    protected OneWayPlatformCollisionController oneWayPlatformCollisionController => 
        m_oneWayPlatformCollision ??= core.GetCoreComponent<OneWayPlatformCollisionController>();
    
    private Movement m_movement;
    private OneWayPlatformCollisionController m_oneWayPlatformCollision;

    private float m_duration;

    public PlayerDropDownState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
        m_duration = data.dropThroughDuration;
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
            var inAirState = fsm.GetState<PlayerAirState>();
            inAirState.StartCoyoteTime();
            fsm.ChangeState<PlayerAirState>();
        }
    }
}