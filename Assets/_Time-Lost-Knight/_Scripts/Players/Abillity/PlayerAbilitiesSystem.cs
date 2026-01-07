using UnityEngine;

public class PlayerAbilitiesSystem : PlayerSystemBase
{
    [SerializeField] private PlayerInputHandler m_input;
    [SerializeField] private CoroutineRunner m_coroutineRunner;
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private PlayerMovementController movement;

    private AbilitiesContainer m_container;

    private WalkAbility walk;
    private JumpAbility jump;
    private DashAbility dash;

    protected override void OnInitialize()
    {
        var movement = GetComponent<PlayerMovementController>();

        m_container = new AbilitiesContainer();

        walk = new WalkAbility(movement, m_playerData);
        jump = new JumpAbility(movement, m_playerData);
        dash = new DashAbility(movement, m_coroutineRunner, m_playerData);

        m_container.RegisterAbility(walk, "Walk");
        m_container.RegisterAbility(jump, "Jump");
        m_container.RegisterAbility(dash, "Dash");

        m_input.move += OnMove;
        m_input.jump += jump.DoJump;
        m_input.dash += dash.DoDash;
    }

    private void Update()
    {
        m_container.UpdateAllAbilities();
    }

    private void OnMove(Vector2 dir)
    {
        walk.DoWalk(dir);
        dash.SetMoveInput(dir);
        movement.UpdateFacing(dir.x);
    }

    public void EnableAbility(string key) =>
        m_container.ActivateAbility(key);

    public void DisableAbility(string key) =>
        m_container.DeactivateAbility(key);
}