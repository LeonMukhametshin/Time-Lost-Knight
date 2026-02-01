using UnityEngine;

public sealed class InteractionInputHandler : MonoBehaviour
{
    public InteractionInputHandler(
       GameInput input,
       InteractionController interactionController)
    {
        input.Player.Interact.performed += _ =>
        {
            interactionController.Interact();
        };
    }
}
