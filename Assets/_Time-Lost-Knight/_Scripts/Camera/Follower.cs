using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector3 m_offcet;

    private void LateUpdate()
    {
        if(m_target is null)
        {
            return;
        }

        transform.position = m_target.position + m_offcet;
    }
    
    public void SetTarget(Transform target)
    {
        if (target is null || m_target == target)
        {
            return;
        }

        m_target = target;
    }
        
}
