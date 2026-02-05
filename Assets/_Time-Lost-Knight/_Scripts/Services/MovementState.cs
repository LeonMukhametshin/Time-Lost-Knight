public class MovementState
{
    protected readonly MovementStateMachine fsm;

    public MovementState(MovementStateMachine fsm)
    {
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FixedUpdate() { }
}