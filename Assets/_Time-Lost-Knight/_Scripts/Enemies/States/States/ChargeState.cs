using UnityEngine;

public class ChargeState : EnemyState
{
    protected ChargeStateData data;
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    protected FlipContoller flipContoller
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    private EnemyCollisionDetector enemyCollisionDetector
    {
        get => m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    }

    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public ChargeState(EnemyFSM fsm, Entity entity, 
        string animBoolName, ChargeStateData data) 
        : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        movement.SetVelocity(data.chargeSpeed, Vector2.right, flipContoller.facingDirection);
    }

    public override void Update()
    {
        base.Update();

        if(Time.time >= startTime + data.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        isDetectingLedge = enemyCollisionDetector.CheckLedge();
        isDetectingWall = enemyCollisionDetector.CheckWallTouch();
        performCloseRangeAction = enemyCollisionDetector.CheckPlayerInCloseRangeAction();
    }
}