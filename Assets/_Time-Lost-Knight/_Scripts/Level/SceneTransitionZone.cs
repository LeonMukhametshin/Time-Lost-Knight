using Game.Core;
using Game.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level
{
    [RequireComponent(typeof(Collider2D))]
    [MovedFrom("")]
    public class SceneTransitionZone : MonoBehaviour
    {
        [Header("Scene")]
        [SerializeField] private string m_sceneName;
        [SerializeField] private LoadSceneMode m_loadMode = LoadSceneMode.Single;
        [SerializeField] private bool m_resetTimeScaleOnLoad = true;

        [Header("Slow Motion")]
        [SerializeField][Min(0f)] private float m_slowdownDuration = 0.6f;
        [SerializeField][Range(0.01f, 1f)] private float m_targetTimeScale = 0.1f;
        [SerializeField][Min(0f)] private float m_delayBeforeLoad = 0.1f;
        [SerializeField] private AnimationCurve m_slowdownCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [Header("Fade")]
        [SerializeField] private ScreenFader m_fader;
        [SerializeField][Min(0f)] private float m_fadeOutDuration = 0.5f;
        [SerializeField][Min(0f)] private float m_fadeDelay = 0f;

        private bool m_isTriggered;
        private float m_defaultFixedDeltaTime;

        private void Awake()
        {
            m_defaultFixedDeltaTime = Time.fixedDeltaTime;
            ResolveFader();
        }

        private void Reset()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_isTriggered)
                return;

            if (!other.CompareTag(Tags.Player))
                return;

            if (string.IsNullOrWhiteSpace(m_sceneName))
            {
                Debug.LogWarning($"{nameof(SceneTransitionZone)} on {name} has empty scene name.");
                return;
            }

            m_isTriggered = true;
            StartCoroutine(TransitionRoutine());
        }

        private void ResolveFader()
        {
            if (m_fader == null)
            {
                m_fader = FindFirstObjectByType<ScreenFader>();
            }
        }

        private IEnumerator TransitionRoutine()
        {
            var startTimeScale = Time.timeScale;
            var duration = Mathf.Max(0.01f, m_slowdownDuration);
            var t = 0f;

            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                var normalized = Mathf.Clamp01(t / duration);
                var curveValue = m_slowdownCurve.Evaluate(normalized);

                Time.timeScale = Mathf.Lerp(startTimeScale, m_targetTimeScale, curveValue);
                Time.fixedDeltaTime = m_defaultFixedDeltaTime * Time.timeScale;

                yield return null;
            }

            Time.timeScale = m_targetTimeScale;
            Time.fixedDeltaTime = m_defaultFixedDeltaTime * Time.timeScale;

            if (m_fader != null && m_fadeOutDuration > 0f)
            {
                yield return m_fader.FadeOut(m_fadeOutDuration);
            }

            if (m_fadeDelay > 0f)
            {
                yield return new WaitForSecondsRealtime(m_fadeDelay);
            }

            if (m_delayBeforeLoad > 0f)
                yield return new WaitForSecondsRealtime(m_delayBeforeLoad);

            if (m_resetTimeScaleOnLoad)
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = m_defaultFixedDeltaTime;
            }

            if (m_loadMode == LoadSceneMode.Single)
            {
                SceneManager.LoadScene(m_sceneName);
            }
            else
            {
                SceneManager.LoadSceneAsync(m_sceneName, m_loadMode);
            }
        }
    }
}
