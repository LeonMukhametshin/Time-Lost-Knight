using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [MovedFrom("")]
    public class ScreenFader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_group;
        [SerializeField] private bool m_fadeInOnStart = true;
        [SerializeField][Min(0f)] private float m_fadeInDuration = 0.6f;
        [SerializeField][Range(0f, 1f)] private float m_startAlpha = 1f;
        [SerializeField] private bool m_ignoreTimeScale = true;

        private void Awake()
        {
            if (m_group == null)
            {
                m_group = GetComponent<CanvasGroup>();
            }

            SetAlpha(m_startAlpha);

            if (m_fadeInOnStart)
            {
                StartCoroutine(FadeTo(0f, m_fadeInDuration));
            }
        }

        public IEnumerator FadeOut(float duration) =>
            FadeTo(1f, duration);

        public IEnumerator FadeIn(float duration) =>
            FadeTo(0f, duration);

        public IEnumerator FadeTo(float targetAlpha, float duration)
        {
            if (m_group == null)
                yield break;

            if (duration <= 0f)
            {
                SetAlpha(targetAlpha);
                yield break;
            }

            var startAlpha = m_group.alpha;
            var t = 0f;

            while (t < duration)
            {
                t += m_ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
                var normalized = Mathf.Clamp01(t / duration);
                var alpha = Mathf.Lerp(startAlpha, targetAlpha, normalized);
                SetAlpha(alpha);
                yield return null;
            }

            SetAlpha(targetAlpha);
        }

        public void SetAlpha(float alpha)
        {
            if (m_group == null)
                return;

            m_group.alpha = alpha;
            m_group.blocksRaycasts = alpha > 0.001f;
            m_group.interactable = false;
        }
    }
}
