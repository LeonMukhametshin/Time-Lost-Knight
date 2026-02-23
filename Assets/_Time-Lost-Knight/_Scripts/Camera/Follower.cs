using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector3 m_offcet;

    private void LateUpdate()
    {
        transform.position = m_target.position + m_offcet;
    }        
}