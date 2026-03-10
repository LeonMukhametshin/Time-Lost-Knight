public class PlayerState : EntityState, IAnimationTrigger
{
    protected Player player;

    protected bool isAnimationFinished;
    protected bool isExitingState;
    public bool isUnlocked { get; private set; }

    protected PlayerData data;

    public PlayerState(
        EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active ) 
        : base(fsm, core, animBoolName)
    {
        this.player = player;
        this.data = data;
        this.isUnlocked = active;
    }

    public override void Enter()
    {
        base.Enter();

        player.animator.SetBool(animBoolName, true);
        
        isAnimationFinished = false;
        isExitingState = false;
    } 
      
    public override void Exit()
    {
        player.animator.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void TriggerAnimation() { }

    public virtual void FinishAnimation() =>
        isAnimationFinished = true;

    public virtual void Unlock() => 
        isUnlocked = true;

    public virtual void Lock() => 
        isUnlocked = false;
}