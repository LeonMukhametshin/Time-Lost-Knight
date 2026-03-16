using Game.Observer;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.Effects
{
    [Serializable]
    [MovedFrom("")]
    public class InteractionAnimationHandler : IObserver
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private string m_triggerName = "Interact";

        public void Notify()
        {
            m_animator.SetTrigger(m_triggerName);
        }
    }
}
