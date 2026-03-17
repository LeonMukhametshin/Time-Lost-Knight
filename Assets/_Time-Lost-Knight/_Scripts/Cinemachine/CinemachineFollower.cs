using Game.Core.ServiceLocatorSpace;
using Game.Player;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Camera.Cinemachine
{
    [MovedFrom("")]
    public class CinemachineFollower : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera m_cinemachineCamera;

        private void Start()
        {
            var target = ServiceLocator.Get<PlayerController>().transform;
            m_cinemachineCamera.Follow = target;
            m_cinemachineCamera.Follow = target;
        }
    }
}
