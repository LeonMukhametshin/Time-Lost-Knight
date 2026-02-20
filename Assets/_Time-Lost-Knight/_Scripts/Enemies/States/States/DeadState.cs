using UnityEngine;

public class DeadState : EnemyState
{
    protected DeadStateData data;

    public DeadState(float startTime, string animBoolName, 
        Entity entity, DeadStateData data) 
        : base(startTime, animBoolName, entity)
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