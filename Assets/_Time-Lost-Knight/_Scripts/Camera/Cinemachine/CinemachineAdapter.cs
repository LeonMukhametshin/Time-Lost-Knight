using Unity.Cinemachine;
using UnityEngine;

public sealed class CinemachineAdapter : MonoBehaviour
{
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private CinemachineCamera m_virtualCamera;

    [SerializeField][Range(-1f, 1f)] private float m_leftThird = 0.33f;
    [SerializeField][Range(-1f, 1f)] private float m_rightThird = 0.66f;
    [SerializeField][Range(0f, 1f)] private float m_center = 0.5f;

    [SerializeField][Min(0f)] private float m_screenXSmoothTime = 0.18f;
    [SerializeField][Min(0f)] private float m_centerSmoothTime = 0.12f;

    private CinemachinePositionComposer m_composer;
    private float m_targetX;
    private float m_velocityX;

    private void Awake()
    {
        if (m_virtualCamera == null)
        {
            enabled = false;
            return;
        }

        m_composer = m_virtualCamera
            .GetCinemachineComponent(CinemachineCore.Stage.Body)
            as CinemachinePositionComposer;

        if (m_composer == null)
        {
            enabled = false;
            return;
        }

        m_targetX = m_composer.Composition.ScreenPosition.x;
    }

    private void OnEnable()
    {
        if (m_cameraController != null)
            m_cameraController.ModeChanged += OnCameraModeChanged;
    }

    private void OnDisable()
    {
        if (m_cameraController != null)
            m_cameraController.ModeChanged -= OnCameraModeChanged;
    }

    private void Update()
    {
        if (m_composer == null) return;

        var settings = m_composer.Composition;
        float currentX = settings.ScreenPosition.x;

        float smoothTime = Mathf.Approximately(m_targetX, m_center) ? m_centerSmoothTime : m_screenXSmoothTime;
        float newX = Mathf.SmoothDamp(currentX, m_targetX, ref m_velocityX, smoothTime);

        settings.ScreenPosition = new Vector2(newX, settings.ScreenPosition.y);
        m_composer.Composition = settings;
    }

    private void OnCameraModeChanged(CameraMode mode)
    {
        switch (mode)
        {
            case CameraMode.LeftThird:
                m_targetX = m_leftThird;
                break;
            case CameraMode.RightThird:
                m_targetX = m_rightThird;
                break;
            case CameraMode.Center:
                m_targetX = m_center;
                break;
            case CameraMode.Locked:
                m_targetX = m_composer.Composition.ScreenPosition.x;
                break;
            default:
                m_targetX = m_center;
                break;
        }
    }

    public void CenterCameraSmoothly(float smoothTime)
    {
        m_targetX = m_center;
        m_screenXSmoothTime = smoothTime;
    }

}


