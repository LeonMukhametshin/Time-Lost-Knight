using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Vector3 m_offcet;
    private Transform m_target;

    public void Start()
    {
        m_target = ServiceLocator.Get<Player>().transform;
    }

    private void LateUpdate()
    {
        if (m_target is not null)
        {
            transform.position = m_target.position + m_offcet;
        }
    }        
}