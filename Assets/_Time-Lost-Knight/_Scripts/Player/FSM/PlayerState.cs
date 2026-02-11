using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerFSM fsm;
    protected PlayerData data;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string animBoolName;

    public PlayerState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.fsm = fsm;
        this.data = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoCheck();
        player.animator.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;

        Debug.Log(animBoolName);
    } 
      
    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
        isExitingState = true;
    }
        
    public virtual void Update() { }

    public virtual void FixedUpdate() => 
        DoCheck();

    public virtual void DoCheck() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTriger() =>
        isAnimationFinished = true;
}