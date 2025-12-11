using UnityEngine;

public class TestAbility : MonoBehaviour
{
    private IAbilitiesContainer abilityContainer;

    private void Start()
    {
        abilityContainer = GetComponent<IAbilitiesContainer>(); 
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
