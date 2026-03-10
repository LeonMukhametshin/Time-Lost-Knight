using UnityEngine;

public class Trap : MonoBehaviour, IPauseHandler
{
    [field: SerializeField] protected Animator animator { get; private set; }

    public virtual void Activate() =>
         animator.SetTrigger(TrapAnimationConsts.ACTIVATE);

    protected virtual void ApplyEffects(Collider2D collision) { }

    private void Start() => 
        ServiceLocator.Get<Pause>().Add(this);

    public virtual void IsPuased(bool isPaused) => 
        animator.enabled = !isPaused;
}