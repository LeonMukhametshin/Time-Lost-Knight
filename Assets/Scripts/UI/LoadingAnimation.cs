using DG.Tweening;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField][Range(0f,5f)] private float m_duration = 2f;

    private Tween m_rotationTween;

    private void OnEnable()
    {
        PlayRotation();
    }

    private void OnDisable()
    {
        m_rotationTween?.Kill();
    }

    private void PlayRotation()
    {
        m_rotationTween = transform.
          DORotate(new Vector3(0, 0, -360f), m_duration, RotateMode.FastBeyond360)
          .SetRelative(true)
          .SetEase(Ease.Linear)
          .SetLoops(-1);
    }
}