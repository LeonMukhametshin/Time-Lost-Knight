using UnityEngine;

public class Entity : MonoBehaviour, IPauseHandler
{
    public EntityFSM fsm { get; protected set; }

    [field: SerializeField] public Core core { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public AnimationToFSM animationToFSM { get; private set; }

    [field: SerializeField] protected EntityData data { get; private set; }

    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();
   
    private Movement m_movement;

    private void OnEnable() => 
        ServiceLocator.Get<Pause>().Add(this);

    private void OnDisable() => 
        ServiceLocator.Get<Pause>().Remove(this);

    public virtual void Awake() =>
        fsm = new EntityFSM();

    public virtual void Update()
    {
        if (ServiceLocator.Get<Pause>().isPaused)
        {
            return;
        }

        fsm.Update();
        core.Update();

        animator.SetFloat(EnemyAnimationConst.Y_VELOCITY, movement.rb.linearVelocityY);
    }

    public virtual void FixedUpdate()
    {
        if (ServiceLocator.Get<Pause>().isPaused)
        {
            return;
        }

        fsm.FixedUpdate();
    }

    public void IsPuased(bool isPaused)
    {
        movement.SetPaused(isPaused);
        animator.enabled = !isPaused;
    }
}