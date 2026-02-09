using UnityEngine;

public class State 
{
    protected FSM fsm;

    protected Entity entity;
    protected float startTime;

    protected string animBoolName;

    public State(FSM fsm, Entity entity, string animBoolName)
    {
        this.fsm = fsm;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() 
    {
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.animator.SetBool(animBoolName, false);
    }

    public virtual void Update() { }

    public virtual void FixedUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
}