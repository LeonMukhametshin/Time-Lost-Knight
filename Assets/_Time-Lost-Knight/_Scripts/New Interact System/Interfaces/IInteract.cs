using UnityEngine;

public interface IInteract
{
    Transform position { get; }
    string displayName { get; }

    bool CanInteract();
    void Interact();
    void OnFocusGained();
    void OnFocusLost();
}