using UnityEngine;

public class Player : MonoBehaviour
{
    public Core core { get; private set;  }

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
    [field: SerializeField] public PlayerInventory inventory { get; private set; }

    [Header("       ----  CHECKERS  ----")][Space(10)]
    [SerializeField] private CheckTransfomsRef m_checkTransfom;

    public StatesContainer statesContainer { get; set; }
    
    public ColliderController colliderController { get; private set; }

    private void Awake() =>
        InitializeComponents();

    private void InitializeComponents()
    {
        var movement = new Movement(m_rigidbody);
        var flip = new FlipContoller(transform);
        var collisionDetector = new CollisionDetector(m_data.checkersData,
            m_checkTransfom, m_data.standColliderHeight);

        core = new Core(movement,
            flip,
            collisionDetector);

        statesContainer = new StatesContainer(this, m_data);
        animationController.Initialize(m_animator, statesContainer);

        statesContainer.SetBaseState();

        statesContainer.GetState<PlayerPrimaryAttackState>()
            .SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        //statesContainer.GetState<PlayerSecondaryAttackState>().SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);
    }

    private void Update()
    {
        core.movement.Update();
        statesContainer.fsm.Update();
    }

    private void FixedUpdate()
    {
        statesContainer.fsm.FixedUpdate();
    }
}