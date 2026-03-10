using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private CinemachineCamera[] m_cameras;

    public void SetTarget(Transform target)
    {
        foreach (var cinemachineCamera in m_cameras)
        {
            cinemachineCamera.Follow = target;
            cinemachineCamera.LookAt = target;
        }
    }

    public void SetCameraState(int state)
    {
        m_animator.SetInteger("CameraState", state);
    }
}