public class PlayerState : EntityState
{
    protected Player player;

    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected PlayerData data;

    public PlayerState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, PlayerData data) 
        : base(fsm, core, animBoolName)
    {
        this.player = player;
        this.data = data;
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
}