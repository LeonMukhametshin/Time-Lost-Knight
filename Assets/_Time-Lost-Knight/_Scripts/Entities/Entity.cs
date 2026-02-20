using UnityEngine;

public class Entity : MonoBehaviour
{
    public FSM fsm;

    public EntityData data;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement m_movement;

    [field: SerializeField] public Core core { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public AnimationToFSM animationToFSM { get; private set; }

    public int lastDamageDirection { get; private set; }

    protected bool isStunned;

    private float m_lastDamageTime;

    public virtual void Awake()
    {
        fsm = new FSM();
    }

    public virtual void Update()
    {
        fsm.currentState.Update();

        animator.SetFloat(EnemyAnimationConst.Y_VELOCITY, movement.rigidbody2D.linearVelocityY);

        if(Time.time >= m_lastDamageTime + data.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate() =>
        fsm.currentState.FixedUpdate();

    public virtual void ResetStunResistance()
    {
        isStunned = false;
    }
}