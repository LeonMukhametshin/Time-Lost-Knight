using Game.Core.CoreComponents;
using Game.Core.Game.Gameplay.Root;
using Game.Interaction.NewSystem;
using Game.Player;
using Game.Player.FSM;
using Game.Player.Input;
using Game.UI;
using Game.UI.CoreSystem;
using Game.UI.Input;
using UnityEngine.Scripting.APIUpdating;
using Game.Core.ServiceLocatorSpace;

namespace Game.Core.Infrastructure.States
{
    [MovedFrom("")]
    public class GameplayState : IState
    {
        private CameraManager m_cameraManager;
        private PlayerHealthBarView m_healthBarView;

        private Pause m_pause;
        private PauseWindow m_pauseWindow;

        private PlayerInputHandler m_playerInputHandler;
        private UIInputHandler m_uIInputHandler;
        private ParticleManager m_particleManager;
        private InteractPrompt m_interactPrompt;
        private PlayerInteractor m_interactor;
        private EndGameWindow m_endGameWindow;

        private PlayerController m_player;

        public GameplayState(StateMachine stateMachine,
            CameraManager cameraManager,
            PlayerHealthBarView healthBarView,
            Pause pause, PauseWindow pauseWindow,
            PlayerInputHandler playerInputHandler,
            UIInputHandler uIInputHandler,
            ParticleManager particleManager,
            InteractPrompt interactPrompt,
            EndGameWindow endGameWindow)
        {
            m_cameraManager = cameraManager;
            m_healthBarView = healthBarView;
            m_pause = pause;
            m_pauseWindow = pauseWindow;
            m_playerInputHandler = playerInputHandler;
            m_uIInputHandler = uIInputHandler;
            m_particleManager = particleManager;
            m_interactPrompt = interactPrompt;
            m_endGameWindow = endGameWindow;
        }

        public void Enter()
        {
            var playerPosition = ServiceLocator.Get<PlayerSpawnpoint>();
			ServiceLocator.Get<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_player = ServiceLocator.Get<IPlayerFactory>().Create();
            m_player.Initialize(m_playerInputHandler);

			ServiceLocator.Register<PlayerFSM>(m_player.fsm as PlayerFSM);

            m_cameraManager.SetTarget(m_player.transform);
            ServiceLocator.Register<CameraManager>(m_cameraManager);

            m_healthBarView.Initialize(m_player.core.GetCoreComponent<HealthComponent>());

            ServiceLocator.Register(m_interactPrompt);
            ServiceLocator.Register(m_particleManager);
            ServiceLocator.Register(m_pause);

            m_interactor = m_player.gameObject.GetComponentInChildren<PlayerInteractor>();
            m_interactor.Initialize(m_playerInputHandler, m_interactPrompt);

            m_pauseWindow.Initialize(m_uIInputHandler);
            m_endGameWindow.Initialize(m_player);
        }

        public void Exit() { }
    }
}