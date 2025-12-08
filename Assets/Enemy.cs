using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_health;

    private void Update()
    {
        if(m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit(float damage)
    {
        m_health -= damage;
    }
}
