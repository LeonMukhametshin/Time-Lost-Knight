using UnityEngine;

public class LookForPlayerState : EnemyState
{
    protected LookForPlayerStateData data;

    protected bool turnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    private Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    private FlipContoller flipController
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

    public LookForPlayerState(EnemyFSM fsm, Entity entity,
        string animBoolName, LookForPlayerStateData data)
        : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        movement.SetVelocityX(0);
    }

    public override void Update()
    {
        base.Update();

        if (turnImmediately)
        {
            flipController.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if ((Time.time >= lastTurnTime + data.timeBetweenTurns && !isAllTurnsDone))
        {
            flipController.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= data.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + data.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public void SetTurnImmediately(bool flip) =>
         turnImmediately = flip;
}