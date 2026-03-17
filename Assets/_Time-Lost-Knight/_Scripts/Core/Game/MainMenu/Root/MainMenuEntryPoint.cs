using Game.Core.Game.MainMenu.Root.View;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.Game.MainMenu.Root
{
    [MovedFrom("")]
    public class MainMenuEntryPoint : MonoBehaviour
    {
        public event Action GoToGameplaySceneRequested;

        [SerializeField] private UIMainMenuRootBinder m_sceneUIRoot;
        private bool m_isInitialized;

        public void Run()
        {
            if (m_isInitialized)
            {
                return;
            }

            if (m_sceneUIRoot == null)
            {
                return;
            }

            m_isInitialized = true;

            m_sceneUIRoot.GoToMainMenuButtonClicked -= OnGoToGameplayRequested;
            m_sceneUIRoot.GoToMainMenuButtonClicked += OnGoToGameplayRequested;
        }

        private void OnGoToGameplayRequested()
        {
            GoToGameplaySceneRequested?.Invoke();
        }
    }
}
