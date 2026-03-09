using UnityEngine;

public class WallGrabAbilityUnlock : MonoBehaviour
{
    private Player m_player;

    private void Awake()
    {
        m_player = ServiceLocator.Get<Player>();
    }

    public void PickUp()
    {
        if (m_player == null || m_player.canGrab == true)
        {
            return;
        }

        m_player.canGrab = true;
        Destroy(gameObject);
    }
}
