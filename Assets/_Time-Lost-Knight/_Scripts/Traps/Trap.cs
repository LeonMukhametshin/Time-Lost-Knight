using UnityEngine;

public class Trap : MonoBehaviour, IPauseHandler
{
    [field: SerializeField] [Range(0,1000)] protected float damage { get; private set; }
    [field: SerializeField] protected Animator animator { get; private set; }

    public virtual void Activate() =>
         animator.SetTrigger(TrapAnimationConsts.ACTIVATE);

    public virtual void ApplyEffects(Collider2D collision) { }

    private void OnEnable() => 
        Pause.instants.Add(this);

    private void OnDisable() => 
        Pause.instants.Remove(this);

    public virtual void IsPuased(bool isPaused) => 
        animator.enabled = !isPaused;
}