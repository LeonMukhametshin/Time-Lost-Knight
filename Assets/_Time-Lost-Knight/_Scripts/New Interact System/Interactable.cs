using Game.Interaction.NewSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.NewSystem
{
    [MovedFrom("")]
    public class Interactable : MonoBehaviour, IInteract
    {
        [SerializeField] private string m_displayName = "Interact";
        [SerializeField] private bool m_isEnabled = true;

        [SerializeField] private UnityEvent m_onIntarect;

        public Transform position => transform;

        public string displayName => m_displayName;

        private void Awake()
        {
            //TODO outline
        }

        public bool CanInteract() =>
            m_isEnabled;

        public void Interact() =>
            m_onIntarect?.Invoke();

        public void OnFocusGained()
        {
            //TODO outline
        }

        public void OnFocusLost()
        {
            //TODO outline
        }
    }
}

