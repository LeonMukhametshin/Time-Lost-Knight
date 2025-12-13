using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AbilitiesContainer m_abilitiesContainer;

    private void OnValidate()
    {
        if(m_abilitiesContainer is null)
        {
            m_abilitiesContainer = GetComponent<AbilitiesContainer>();
        }
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