using UnityEngine;

public class TestAbility : MonoBehaviour
{
    private AbilitiesContainer abilityContainer;

    private void Start()
    {
        abilityContainer = GetComponent<AbilitiesContainer>(); 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Activate DASH!");
            abilityContainer.ActivateAbility("Dash");
            Destroy(this);
        }
    }
}
