using System;
using UnityEngine;

public interface IInteractable
{
    int priority { get; }
    Transform interactionPoint { get; }

    void Interact();
    bool CanInteract();
}