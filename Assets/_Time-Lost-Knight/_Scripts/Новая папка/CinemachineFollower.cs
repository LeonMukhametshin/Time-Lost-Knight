using Unity.Cinemachine;
using UnityEngine;

public class CinemachineFollower : MonoBehaviour
{
    [SerializeField] private CinemachineCamera m_cinemachineCamera;

    private void Start()
    {
        var target = ServiceLocator.Get<Player>().transform;
        m_cinemachineCamera.Follow = target;
        m_cinemachineCamera.Follow = target;
    }
}