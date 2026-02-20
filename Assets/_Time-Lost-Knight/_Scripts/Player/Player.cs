using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Core core { get; private set; }

    [SerializeField] private PlayerData m_data;



    [Header("       ----  PLAYER DATA  ----")]
    [Space(10)]

    [field: Header("       ----  OTHER COMPONENTS  ----")]
    [field: Space(10)]
    [field: SerializeField] public PlayerInventory inventory { get; private set; }
    [field: SerializeField] public DashVizualizer dashVizualizer { get; private set; }
    [field: SerializeField] public PlayerInputHandler inputHandler { get; private set; }
    [field: SerializeField] public PlayerAnimationController animationController { get; private set; }

    public StatesContainer statesContainer { get; set; }

    private void Awake()
    {
        statesContainer = new StatesContainer(this, m_data);
        animationController.Initialize(statesContainer);

        statesContainer.SetBaseState();

        statesContainer.GetState<PlayerPrimaryAttackState>()
            .SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        statesContainer.GetState<PlayerSecondaryAttackState>()
            .SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);
    }

    private void Update()
    {
        core.Update();
        statesContainer.fsm.Update();
        statesContainer.fsm.FixedUpdate();
    }
}