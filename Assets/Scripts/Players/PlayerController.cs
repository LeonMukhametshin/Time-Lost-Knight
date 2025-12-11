using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IAbilitiesContainer m_abilitiesContainer;

    private void Start()
    {
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