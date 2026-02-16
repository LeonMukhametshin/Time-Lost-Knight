using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("       ----  PLAYER DATA  ----")][Space(10)]
    [SerializeField] private PlayerData m_data;

    [Header("       ----  UNITY COMPONENTS  ----")][Space(10)]
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private BoxCollider2D m_collider;

    [field: Header("       ----  OTHER COMPONENTS  ----")][field: Space(10)]
    [field: SerializeField] public PlayerInputHandler inputHandler { get; private set; }
    [field: SerializeField] public DashVizualizer dashVizualizer { get; private set; }
    [field: SerializeField] public PlayerAnimationController animationController { get; private set; }

    [Header("       ----  CHECKERS  ----")][Space(10)]
    [SerializeField] private CheckTransfomRef m_checkTransfom;

    public Movement movement { get; private set; }
    public StatesContainer statesContainer { get; set; }
    public FlipContoller flipController { get; private set; }
    public CollisionDetector collisionDetector { get; private set; }
    public ColliderController colliderController { get; private set; }


    private void Awake() =>
        ResolveReferences();

    private void ResolveReferences()
    {
        if(m_animator == null)
        {
            m_animator = GetComponent<Animator>();
        }

        if(m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        if(m_collider == null)
        {
            m_collider = GetComponent<BoxCollider2D>();
        }

        if(inputHandler == null)
        {
            inputHandler = GetComponent<PlayerInputHandler>();
        }

        if(animationController == null)
        {
            animationController = GetComponent<PlayerAnimationController>();
        }

        if(animationController == null)
        {
            animationController = gameObject.AddComponent<PlayerAnimationController>();
        }

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        movement = new Movement(m_rigidbody);
        colliderController = new ColliderController(m_collider);
        collisionDetector = new CollisionDetector(m_data.checkersData, m_checkTransfom, m_data.standColliderHeight);
        flipController = new FlipContoller(transform, collisionDetector);
 
        statesContainer = new StatesContainer(this, m_data);
        animationController.Initialize(m_animator, statesContainer);

        statesContainer.SetBaseState();
    }

    private void Update()
    {
        movement.Update();
        statesContainer.fsm.Update();
    }

    private void FixedUpdate()
    {
        statesContainer.fsm.FixedUpdate();
    }
}
