public class PlayerState : EntityState
{
    protected Player player;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    public PlayerState(EntityFSM fsm, Core core, 
        string animBoolName, Player player) 
        : base(fsm, core, animBoolName)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();

        player.animationController.animator.SetBool(animBoolName, true);
        
        isAnimationFinished = false;
        isExitingState = false;
    } 
      
    public override void Exit()
    {
        player.animationController.animator.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTriger() =>
        isAnimationFinished = true;
}