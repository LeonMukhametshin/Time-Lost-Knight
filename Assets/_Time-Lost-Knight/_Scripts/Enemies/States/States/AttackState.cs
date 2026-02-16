using System;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public AttackState(FSM fsm, Entity entity, string animBoolName, Transform attackPosition) : base(fsm, entity, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.animationToFSM.attackState = this;
        isAnimationFinished = false;
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}