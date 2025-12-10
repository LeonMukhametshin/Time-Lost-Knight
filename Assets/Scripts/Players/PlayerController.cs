using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IPlayerInput m_input;
    [SerializeField] private ICharacterMovement m_movement;
    [SerializeField] private IAbilitiesContainer m_abilitiesContainer;

    private void Awake()
    {
        m_input = GetComponent<IPlayerInput>();
        m_movement = GetComponent<ICharacterMovement>();
        m_abilitiesContainer = GetComponent<IAbilitiesContainer>();
    }

    private void Update()
    {
        m_abilitiesContainer?.UpdateAllAbilities();
    }

    private void FixedUpdate()
    {
        m_abilitiesContainer?.UpdateAllAbilitiesFixed();
    }
}
