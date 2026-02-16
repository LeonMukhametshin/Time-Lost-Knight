using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomCameraBounds : MonoBehaviour
{
    [SerializeField] private Collider2D m_roomCollider;
    [SerializeField] private MonoBehaviour m_confiner;
    [SerializeField][Min(0f)] private float m_centerSmoothTime = 0.15f;

    private Component m_adapter;
    private bool m_isSwitching;
    private PropertyInfo m_boundingShapeProperty;
    private MethodInfo m_invalidateCacheMethod;
    private MethodInfo m_centerCameraSmoothlyMethod;

    private void Awake()
    {
        CacheMembers();
    }

    private void OnValidate()
    {
        CacheMembers();
    }

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Tags.Player)) return;
        if (m_confiner == null || m_roomCollider == null) return;

        if (m_boundingShapeProperty == null) return;
        m_boundingShapeProperty.SetValue(m_confiner, m_roomCollider);
        m_invalidateCacheMethod?.Invoke(m_confiner, null);

        if (!m_isSwitching)
        {
            m_centerCameraSmoothlyMethod?.Invoke(m_adapter, new object[] { m_centerSmoothTime });
        }
    }

    private void CacheMembers()
    {
        m_boundingShapeProperty = null;
        m_invalidateCacheMethod = null;
        m_centerCameraSmoothlyMethod = null;
        m_adapter = null;

        if (m_confiner == null) return;

        var type = m_confiner.GetType();
        m_boundingShapeProperty = type.GetProperty("BoundingShape2D", BindingFlags.Instance | BindingFlags.Public);
        m_invalidateCacheMethod = type.GetMethod("InvalidateCache", BindingFlags.Instance | BindingFlags.Public);

        m_adapter = m_confiner.GetComponent("CinemachineAdapter");
        if (m_adapter == null) return;

        m_centerCameraSmoothlyMethod = m_adapter.GetType().GetMethod(
            "CenterCameraSmoothly",
            BindingFlags.Instance | BindingFlags.Public,
            null,
            new[] { typeof(float) },
            null
        );
        if (m_centerCameraSmoothlyMethod == null)
        {
            m_adapter = null;
        }
    }
}
