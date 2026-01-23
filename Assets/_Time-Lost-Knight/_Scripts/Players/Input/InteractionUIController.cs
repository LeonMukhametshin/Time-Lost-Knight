using System;
using UnityEngine;

[Serializable]
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