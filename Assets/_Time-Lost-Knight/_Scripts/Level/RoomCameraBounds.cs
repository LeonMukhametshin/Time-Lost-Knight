using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(Collider2D))]
public class RoomCameraBounds : MonoBehaviour
{
    [SerializeField] private Collider2D m_roomCollider;
    [SerializeField] private CinemachineConfiner2D m_confiner;
    [SerializeField][Min(0f)] private float m_centerSmoothTime = 0.15f;

    private CinemachineAdapter m_adapter;
    private bool m_isSwitching;

    private void Awake() =>
        ResolveReferences();

    private void OnValidate() =>
        ResolveReferences();

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ResolveReferences();
        if (!other.CompareTag(Tags.Player)) return;
        if (m_confiner == null || m_roomCollider == null) return;

        m_confiner.BoundingShape2D = m_roomCollider;
        m_confiner.InvalidateCache();

        if (!m_isSwitching)
        {
            if (m_adapter != null)
                m_adapter.CenterCameraSmoothly(m_centerSmoothTime);
        }
    }

    private void ResolveReferences()
    {
        if (m_roomCollider == null)
            m_roomCollider = GetComponent<Collider2D>();

        if (m_confiner == null)
            m_confiner = FindAnyObjectByType<CinemachineConfiner2D>();

        if (m_confiner == null)
            return;

        if (m_adapter == null || m_adapter.gameObject != m_confiner.gameObject)
            m_adapter = m_confiner.GetComponent<CinemachineAdapter>();
    }
}
