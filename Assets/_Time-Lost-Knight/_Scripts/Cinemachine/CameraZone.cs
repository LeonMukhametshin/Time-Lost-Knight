using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private CameraState m_state;

    private CameraManager m_cameraManager;

    private void Start()
    {
        m_cameraManager = ServiceLocator.Get<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Enter the zone and switch to " + m_state + " " + (int)m_state);
        m_cameraManager.SetCameraState((int)m_state);
    }
}