using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHorizontalMove : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private Rigidbody2D m_rigidbody2D;

    private void OnValidate()
    {
        if(!m_rigidbody2D)
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }     
    }

    public void Move(Vector2 moveDirection)
    {
        Vector2 velocity = moveDirection.normalized * m_speed;
        m_rigidbody2D.linearVelocity = new Vector2(velocity.x, m_rigidbody2D.linearVelocityY);
    }
}