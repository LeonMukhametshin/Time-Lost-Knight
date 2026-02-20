using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    private EnemyCollisionDetector enemyCollisionDetector
    {
        get => m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    }

    private Movement m_movement;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public AttackState(FSM fsm, Entity entity, 
        string animBoolName, Transform attackPosition) 
        : base(fsm, entity, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.animationToFSM.attackState = this;
        isAnimationFinished = false;
        movement.SetVelocityX(0f);
    }

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() =>
        isAnimationFinished = true;
}