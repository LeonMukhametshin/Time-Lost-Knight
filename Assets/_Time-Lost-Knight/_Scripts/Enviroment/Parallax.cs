using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField][Range(0f, 1f)] private float m_paralaxEffect;

    private float m_xPosition;

    private void Awake()
    {
        m_xPosition = transform.position.x;
    }

    private void LateUpdate()
    {
        float distX = (m_camera.transform.position.x * (1 - m_paralaxEffect));
        transform.position = new Vector3(m_xPosition + distX, transform.position.y);
    }
}