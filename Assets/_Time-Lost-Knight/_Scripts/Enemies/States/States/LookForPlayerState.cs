using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

public class LookForPlayerState : State
{
    protected LookForPlayerStateData data;

    protected bool turnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    public LookForPlayerState(FSM fsm, Entity entity, string animBoolName, LookForPlayerStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        entity.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if(turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if ((Time.time >= lastTurnTime + data.timeBetweenTurns && !isAllTurnsDone))
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if(amountOfTurnsDone >= data.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + data.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
