using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityFSM fsm { get; private set; } 
    [field: SerializeField] public Core core { get; private set; }

    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();
    
    private Movement m_movement;

    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public AnimationToFSM animationToFSM { get; private set; }

    public virtual void Awake()
    {
        fsm = new EntityFSM();
    }

    public virtual void Update()
    {
        fsm.Update();
        core.Update();

        animator.SetFloat(EnemyAnimationConst.Y_VELOCITY, movement.rigidbody2D.linearVelocityY);
    }

    public virtual void FixedUpdate() 
    {
        fsm.FixedUpdate();
    }
}