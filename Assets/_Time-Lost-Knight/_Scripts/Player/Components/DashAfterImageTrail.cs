using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.Components
{
    [MovedFrom("")]
    public class DashAfterImageTrail : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] m_sources;
        [SerializeField] private Transform m_spawnRoot;
        [SerializeField] private Color m_tint = new Color(0.35f, 0.35f, 0.35f, 1f);
        [SerializeField][Range(0f, 1f)] private float m_startAlpha = 0.6f;
        [SerializeField][Range(0f, 1f)] private float m_endAlpha = 0f;
        [SerializeField][Min(0.01f)] private float m_spawnInterval = 0.05f;
        [SerializeField][Min(0.01f)] private float m_lifeTime = 0.35f;
        [SerializeField] private bool m_useUnscaledTime;
        [SerializeField] private int m_sortingOrderOffset = 1;

        private bool m_isActive;
        private float m_nextSpawnTime;

        private void OnValidate() =>
            ResolveDefaults();

        private void Awake() =>
            ResolveDefaults();

        private void Update()
        {
            if (!m_isActive)
            {
                return;
            }

            var time = m_useUnscaledTime ? Time.unscaledTime : Time.time;
            if (time < m_nextSpawnTime)
            {
                return;
            }

            SpawnAfterImages();
            m_nextSpawnTime = time + m_spawnInterval;
        }

        public void StartTrail()
        {
            m_isActive = true;
            m_nextSpawnTime = 0f;
            SpawnAfterImages();
        }

        public void StopTrail() =>
            m_isActive = false;

        private void ResolveDefaults()
        {
            if (m_sources == null || m_sources.Length == 0)
            {
                m_sources = GetComponentsInChildren<SpriteRenderer>();
            }

            // Intentionally do not auto-assign m_spawnRoot:
            // leaving it null keeps afterimages in world space.
        }

        private void SpawnAfterImages()
        {
            if (m_sources == null || m_sources.Length == 0)
            {
                return;
            }

            foreach (var source in m_sources)
            {
                if (source == null || source.sprite == null)
                {
                    continue;
                }

                var ghost = new GameObject($"{source.gameObject.name}_DashGhost");
                ghost.transform.position = source.transform.position;
                ghost.transform.rotation = source.transform.rotation;
                ghost.transform.localScale = source.transform.lossyScale;
                if (m_spawnRoot != null)
                {
                    ghost.transform.SetParent(m_spawnRoot, true);
                }

                var spriteRenderer = ghost.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = source.sprite;
                spriteRenderer.flipX = source.flipX;
                spriteRenderer.flipY = source.flipY;
                spriteRenderer.sortingLayerID = source.sortingLayerID;
                spriteRenderer.sortingOrder = source.sortingOrder - m_sortingOrderOffset;

                var baseColor = source.color;
                spriteRenderer.color = new Color(
                    baseColor.r * m_tint.r,
                    baseColor.g * m_tint.g,
                    baseColor.b * m_tint.b,
                    m_startAlpha);

                StartCoroutine(FadeAndDestroy(spriteRenderer));
            }
        }

        private IEnumerator FadeAndDestroy(SpriteRenderer spriteRenderer)
        {
            var startColor = spriteRenderer.color;
            var endColor = new Color(startColor.r, startColor.g, startColor.b, m_endAlpha);

            var t = 0f;
            while (t < m_lifeTime)
            {
                t += m_useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                var normalized = Mathf.Clamp01(t / m_lifeTime);
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.Lerp(startColor, endColor, normalized);
                }
                yield return null;
            }

            if (spriteRenderer != null)
            {
                Destroy(spriteRenderer.gameObject);
            }
        }
    }
}
