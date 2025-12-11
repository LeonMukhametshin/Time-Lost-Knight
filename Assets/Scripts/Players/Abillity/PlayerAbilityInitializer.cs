using UnityEngine;

public class PlayerAbilityInitializer : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private CharacterMovementConfig m_movementConfig;
    [SerializeField] private WalkAbilityConfig m_walkConfig;
    [SerializeField] private JumpAbilityConfig m_jumpConfig;
    [SerializeField] private DashAbilityConfig m_dashConfig;

    [Header("Components")]
    [SerializeField] private CharacterMovement m_movement;
    [SerializeField] private KeyboardPlayerInput m_playerInput;
    [SerializeField] private PlayerAbilitiesContainer m_abilitiesContainer;

    private void OnValidate()
    {
        if(!m_movement)
        {
            m_movement = GetComponent<CharacterMovement>();
        }
        if(!m_playerInput)
        {
            m_playerInput = GetComponent<KeyboardPlayerInput>();
        }
        if(!m_abilitiesContainer)
        {
            m_abilitiesContainer = GetComponent<PlayerAbilitiesContainer>();
        }
    }

    private void Awake()
    {
        RegisterAbilities();
    }

    private void RegisterAbilities()
    {
        var walkAbility = gameObject.AddComponent<WalkAbility>();
        walkAbility.Initialize(m_movement, m_playerInput, m_walkConfig);
        m_abilitiesContainer.RegisterAbility(walkAbility, m_walkConfig.Key);

        var jumpAbility = gameObject.AddComponent<JumpAbility>();
        jumpAbility.Initialize(m_movement, m_playerInput, m_jumpConfig);
        m_abilitiesContainer.RegisterAbility(jumpAbility, m_jumpConfig.Key);

        var dashAbility = gameObject.AddComponent<DashAbility>();
        dashAbility.Initialize(m_movement, m_playerInput, m_dashConfig);
        m_abilitiesContainer.RegisterAbility(dashAbility, m_dashConfig.Key);

        if (m_walkConfig.IsEnabledByDefault)
        {
            m_abilitiesContainer.ActivateAbility(m_walkConfig.Key);
        }
        if (m_jumpConfig.IsEnabledByDefault)
        {
            m_abilitiesContainer.ActivateAbility(m_jumpConfig.Key);
        }
        if(m_dashConfig.IsEnabledByDefault)
        {
            m_abilitiesContainer.ActivateAbility(m_dashConfig.Key);
        }
    }
}