using UnityEngine;

public class WallJumpAbilityUnlock : MonoBehaviour
{
    private Player m_player;

    private void Awake()
    {
        m_player = ServiceLocator.Get<Player>();
    }

    public void PickUp()
    {
        if (m_player == null || m_player.canWallJump == true)
        {
            return;
        }

        m_player.canWallJump = true;
        Destroy(gameObject);
    }
}
