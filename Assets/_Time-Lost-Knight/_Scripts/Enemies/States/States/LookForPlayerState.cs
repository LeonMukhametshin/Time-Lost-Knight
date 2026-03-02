using UnityEngine;

public class LookForPlayerState : EnemyState
{
    protected LookForPlayerStateData data;

    protected bool turnImmediately;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    private Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    private FlipContoller flipController => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    private EnemyCollisionDetector enemyCollisionDetector =>
        m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public LookForPlayerState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        LookForPlayerStateData data) 
        : base(fsm, core, animBoolName, entity)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMaxAgroRange();
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