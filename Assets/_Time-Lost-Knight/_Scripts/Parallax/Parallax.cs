using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_paralaxEffect;

    private float m_xPosition;

    private void Awake()
    {
        m_xPosition = transform.position.x;
    }

    private void LateUpdate()
    {
        float distX = (m_camera.transform.position.x * (1 - m_paralaxEffect));
        transform.position = new Vector2(m_xPosition + distX, transform.position.y);
    }
}