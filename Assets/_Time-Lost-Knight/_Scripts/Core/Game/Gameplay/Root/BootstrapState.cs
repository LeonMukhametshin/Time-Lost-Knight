using Game.Core;
using Game.Core.Infrastructure.States;
using Game.Core.ServiceLocatorSpace;
using Game.Player;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.Game.Gameplay.Root
{
    [MovedFrom("")]
    public class BootstrapState : MonoBehaviour, IState
    {
        public event Action LevelLoaded;

        [SerializeField] private PlayerSpawnpoint m_playerSpawner;

        private StateMachine m_stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            var playerFactory = new PlayerFactory("Prefabs/Player");

			ServiceLocator.Register(m_playerSpawner);
            ServiceLocator.Register<IPlayerFactory>(playerFactory);
            ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);

            LoadLevel();
        }

        public void Exit() { }

        private void LoadLevel()
        {
            SceneManager.LoadSceneAsync(
                SceneNames.LEVEL_EXAMPLE_TWO,
                LoadSceneMode.Additive)
                .completed += _ =>
                {
                    LevelLoaded?.Invoke();
                    m_stateMachine.ChangeState<GameplayState>();
                };
        }
    }
}
