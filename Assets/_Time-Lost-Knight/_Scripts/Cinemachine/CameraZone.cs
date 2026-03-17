using Game.Core.ServiceLocatorSpace;
using Game.Player;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Camera.Cinemachine
{
    [MovedFrom("")]
    public class CameraZone : MonoBehaviour
    {
        [SerializeField] private CameraState m_state;

        private CameraManager m_cameraManager;

        public void Start()
        {
            m_cameraManager = ServiceLocator.Get<CameraManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            m_cameraManager.SetCameraState((int)m_state);
        }
    }
}
