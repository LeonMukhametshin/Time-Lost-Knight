using UnityEngine;

public class WallClimbingAbilityUnlock : MonoBehaviour
{
    private Player m_player;

    private void Awake()
    {
        m_player = ServiceLocator.Get<Player>();
    }

    public void PickUp()
    {
        if (m_player == null || m_player.canClimbing == true)
        {
            return;
        }

        m_player.canClimbing = true;
        Destroy(gameObject);
    }
}
