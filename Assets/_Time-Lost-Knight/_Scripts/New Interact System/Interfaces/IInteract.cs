using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.NewSystem.Interfaces
{
    [MovedFrom("")]
    public interface IInteract
    {
        Transform position { get; }
        string displayName { get; }

        bool CanInteract();
        void Interact();
        void OnFocusGained();
        void OnFocusLost();
    }
}

