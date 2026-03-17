using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Presentation
{
    [RequireComponent(typeof(RectTransform))]
    [MovedFrom("")]
    public class CreditsAutoScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform m_content;
        [SerializeField] private RectTransform m_endMarker;

        [SerializeField] private float m_endY = 1200f;
        [SerializeField][Min(0f)] private float m_speed = 60f;
        [SerializeField][Min(0f)] private float m_startDelay = 0.5f;
        [SerializeField] private bool m_loop;
        [SerializeField][Min(0f)] private float m_loopDelay = 1f;
        [SerializeField] private string m_sceneToLoadOnFinish;
        [SerializeField][Min(0f)] private float m_delayBeforeLoad = 1f;
        [SerializeField] private bool m_useUnscaledTime = true;

        private Vector2 m_startPosition;
        private Sequence m_sequence;

        private void OnValidate()
        {
            if (m_content == null)
            {
                m_content = GetComponent<RectTransform>();
            }
        }

        private void Awake()
        {
            m_startPosition = m_content.anchoredPosition;
            StartScroll();
        }

        private void OnDisable() =>
            KillSequence();

        private void OnDestroy() =>
            KillSequence();

        public void StartScroll()
        {
            KillSequence();

            m_content.anchoredPosition = m_startPosition;

            var endY = GetEndY();
            var distance = Mathf.Abs(endY - m_startPosition.y);
            if (distance <= 0.01f || m_speed <= 0.01f)
            {
                return;
            }

            var duration = distance / m_speed;

            m_sequence = DOTween.Sequence();

            if (m_startDelay > 0f)
            {
                m_sequence.AppendInterval(m_startDelay);
            }

            m_sequence.Append(
                m_content
                    .DOAnchorPosY(endY, duration)
                    .SetEase(Ease.Linear));

            if (m_loop)
            {
                if (m_loopDelay > 0f)
                {
                    m_sequence.AppendInterval(m_loopDelay);
                }

                m_sequence.SetLoops(-1, LoopType.Restart);
                m_sequence.OnRewind(() => m_content.anchoredPosition = m_startPosition);
            }
            else if (!string.IsNullOrWhiteSpace(m_sceneToLoadOnFinish))
            {
                if (m_delayBeforeLoad > 0f)
                {
                    m_sequence.AppendInterval(m_delayBeforeLoad);
                }

                m_sequence.AppendCallback(() => SceneManager.LoadScene(m_sceneToLoadOnFinish));
            }

            if (m_useUnscaledTime)
            {
                m_sequence.SetUpdate(true);
            }
        }

        private float GetEndY() =>
            m_endMarker != null ? m_endMarker.anchoredPosition.y : m_endY;

        private void KillSequence()
        {
            if (m_sequence != null && m_sequence.IsActive())
            {
                m_sequence.Kill();
            }
        }
    }
}
