using UnityEngine;

public class Trap : MonoBehaviour
{
    [field: SerializeField] [Range(0,1000)] protected float damage { get; private set; }
    [field: SerializeField] protected Animator animator { get; private set; }

    public virtual void Activate() =>
         animator.SetTrigger(TrapAnimationConsts.ACTIVATE);

    public virtual void Damage(Collider2D collision) { }
}