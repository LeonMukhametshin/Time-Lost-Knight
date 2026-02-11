using UnityEngine;

public class Player : MonoBehaviour
{
    public StatesContainer statesContainer { get; set; }

    [SerializeField] private PlayerData m_data;

    [SerializeField] private Rigidbody2D m_rigidbody;
    [field: SerializeField] public Animator animator { get; private set; }

    [field: SerializeField] public PlayerInputHandler inputHandler { get; private set; }
    [field: SerializeField] public Movement movement { get; private set; }
    [field: SerializeField] public PlayerCollisionDetector collisionDetector { get; private set; }
    [field: SerializeField] public PlayerColliderController colliderController { get; private set; }
    [field: SerializeField] public PlayerAnimationController animationController { get; private set; }
    [field: SerializeField] public FlipContoller flipController { get; private set; }

    [field: SerializeField] public Transform dashDirectionIndicator {  get; private set; }
  

    private void Awake()
    {
        movement = new Movement(m_rigidbody);

        collisionDetector.Initialize(m_data.groundCheckRadius, 
            m_data.ceilingCheckRadius, m_data.wallCheckDistance, m_data.groundLayer);

        flipController.Initialize(collisionDetector);
     
        statesContainer = new StatesContainer(this, m_data);
        statesContainer.Initalize();

        animationController.Initialize(statesContainer);
    }

    private void Update()
    {
        if (!statesContainer.initialized)
        {
            return;
        }

        movement.Update();
        statesContainer.fsm.currentState.Update();
    }

    private void FixedUpdate()
    {
        if(!statesContainer.initialized)
        {
            return;
        }

        statesContainer.fsm.currentState.FixedUpdate();
    }
}