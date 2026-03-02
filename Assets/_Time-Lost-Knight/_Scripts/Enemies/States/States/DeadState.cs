using UnityEngine;

public class DeadState : EnemyState
{
    protected DeadStateData data;

    public DeadState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        DeadStateData data) 
        : base(fsm, core, animBoolName, entity)
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