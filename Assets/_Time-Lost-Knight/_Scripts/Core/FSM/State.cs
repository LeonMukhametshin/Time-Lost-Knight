using UnityEngine;

public class State : IState
{
    protected FSM fsm;
    protected Core core;
    protected Entity entity;
    
    protected string animBoolName;
    public float startTime { get; protected set; } 

    public State(FSM fsm, Entity entity, string animBoolName)
    {
        this.fsm = fsm;
        this.entity = entity;
        this.animBoolName = animBoolName;
        core = entity.core;
    }

    public virtual void Enter() 
    {
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit() =>
        entity.animator.SetBool(animBoolName, false);

    public virtual void Update() { }

    public virtual void FixedUpdate() =>
          DoChecks();

    public virtual void DoChecks() { }
}