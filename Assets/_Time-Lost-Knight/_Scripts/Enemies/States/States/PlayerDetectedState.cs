using UnityEngine;

public class PlayerDetectedState : State
{
    protected PlayerDetectedData data;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performeLongRangeAction;
    protected bool performeCloseRangeAction;
    protected bool isDetectingLedge;

    private Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    private EnemyCollisionDetector enemyCollisionDetector
    {
        get => m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    }

    private Movement m_movement;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public PlayerDetectedState(FSM fsm, Entity entity, 
        string animBoolName, PlayerDetectedData data) 
        : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = enemyCollisionDetector.CheckLedge();
        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMaxAgroRange();
        performeCloseRangeAction = enemyCollisionDetector.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        performeLongRangeAction = false;
        movement.SetVelocityX(0f);
    }

    public override void Update()
    {
        base.Update();

        if(Time.time >= startTime + data.longRangeActionTime)
        {
            performeLongRangeAction = true;
        }
    }
}