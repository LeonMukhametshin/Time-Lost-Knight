using UnityEngine;

public class DeadState : State
{
    protected DeadStateData data;

    public DeadState(FSM fsm, Entity entity, string animBoolName, DeadStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(data.deathBloodParticle, entity.transform.position, 
            data.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(data.deathBloodParticle, entity.transform.position,
            data.deathBloodParticle.transform.rotation);

        entity.gameObject.SetActive(false);
    }
}