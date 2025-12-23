using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_subject;

    private Vector2 m_startPosition;

    private float m_startZ;

    private Vector2 m_travel => (Vector2)m_camera.transform.position - m_startPosition;

    private float m_distanceFromSubject => transform.position.z - m_subject.position.z;
    private float m_clippingPlane => 
        (m_camera.transform.position.z + (m_distanceFromSubject > 0 ? m_camera.farClipPlane : m_camera.nearClipPlane));

    private float m_parallaxFactor => Mathf.Abs(m_distanceFromSubject) / m_clippingPlane;
    private Vector2 m_parallaxfector;

    private void Start()
    {
        m_startPosition = transform.position;
        m_startZ = transform.position.z;
    }

    private void LateUpdate()
    {
        Vector2 newPosition = m_startPosition + m_travel * m_parallaxFactor;
    
        transform.position = new Vector3(newPosition.x, newPosition.y, m_startZ);
    }
}