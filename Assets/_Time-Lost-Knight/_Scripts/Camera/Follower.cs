using UnityEngine;
using Zenject;

public class Follower : MonoBehaviour
{
    [Inject] private Player m_player;
    [SerializeField] private Vector3 m_offcet;

    private void LateUpdate()
    {
        transform.position = m_player.gameObject.transform.position + m_offcet;
    }        
}