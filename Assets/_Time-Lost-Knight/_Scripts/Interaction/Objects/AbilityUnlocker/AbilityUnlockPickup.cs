using UnityEngine;

public class GrabUnlockPickup : MonoBehaviour
{
    private Player m_player;

    private void Awake()
    {
        m_player = ServiceLocator.Get<Player>();
    }

    public void PickUp()
    {
        if (m_player == null || m_player.canDash == true)
        {
            return;
        }

        m_player.canDash = true;
        Destroy(gameObject);
    }
}

