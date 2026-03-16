using Game.Observer;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.UI
{
    [Serializable]
    [MovedFrom("")]
    public class InteractionUIController : IObserver
    {
        [SerializeField] private InteractionPromptView m_prompt;

        public void Notify()
        {
            m_prompt.Hide();
        }

        public void OnInteractableEnter() =>
            m_prompt.Show();

        public void OnInteractableExit() =>
             m_prompt.Hide();
    }
}
