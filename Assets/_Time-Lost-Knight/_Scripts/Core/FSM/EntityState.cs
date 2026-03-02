using UnityEngine;

public abstract class EntityState : IState, IUpdateState, IFixedUpdateState
{
    protected Core core;
    protected EntityFSM  fsm;

    public float startTime { get; protected set; }
    protected string animBoolName;

    protected EntityState(EntityFSM fsm, Core core, string animBoolName)
    {
        this.fsm = fsm;
        this.animBoolName = animBoolName;
        this.core = core;
    }

    public virtual void Enter() 
    {
        startTime = Time.time;
        DoCheck();
    }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() =>
        DoCheck();

    public virtual void DoCheck() { }
}