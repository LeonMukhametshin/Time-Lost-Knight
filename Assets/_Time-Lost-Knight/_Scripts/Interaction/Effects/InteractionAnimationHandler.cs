using System;
using UnityEngine;

[Serializable]
public class InteractionAnimationHandler : IObserver
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_triggerName = "Interact";

    public void Notify()
    {
        m_animator.SetTrigger(m_triggerName);
    }
}