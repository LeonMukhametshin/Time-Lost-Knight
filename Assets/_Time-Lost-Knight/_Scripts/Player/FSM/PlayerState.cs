using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerFSM fsm;
    protected PlayerData data;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string m_animName;

    public PlayerState(Player player, PlayerFSM fsm, 
        PlayerData data, string animName)
    {
        this.player = player;
        this.fsm = fsm;
        this.data = data;
        this.m_animName = animName;
    }

    public virtual void Enter()
    {
        DoCheck();
        Debug.Log(this + " ENTER");
        player.animationController.animator.SetBool(m_animName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    } 
      
    public virtual void Exit()
    {
        player.animationController.animator.SetBool(m_animName, false);
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