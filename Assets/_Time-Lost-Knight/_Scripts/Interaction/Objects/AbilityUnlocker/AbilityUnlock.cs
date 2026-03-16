using Game.Core.CoreComponents;
using Game.Core.ServiceLocatorSpace;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.Objects.AbilityUnlocker
{
    [MovedFrom("")]
    public class AbilityUnlock : MonoBehaviour
    {
        [SerializeField] private GameObject m_endParticles;
        protected PlayerFSM m_playerFSM { get; private set; }

        private void Start()
        {
            m_playerFSM = ServiceLocator.Get<PlayerFSM>();
            if (m_playerFSM == null)
            {
                throw new System.Exception("PlayerFSM not found in ServiceLocator");
            }
        }

        public virtual void Unlock() 
        {
            ServiceLocator
                .Get<ParticleManager>()
                .StartParticles(m_endParticles);
        }
    }
}