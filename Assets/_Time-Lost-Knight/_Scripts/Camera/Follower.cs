using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Vector3 m_offcet;
    private Transform m_target;

    public void Awake()
    {
        m_target = ServiceLocator.Get<Player>().transform;
    }

    private void LateUpdate()
    {
        transform.position = m_target.position + m_offcet;
    }        
}