using UnityEngine;

public class GrabUnlockPickup : MonoBehaviour
{
    [SerializeField] private PlayerWallGrabState m_abilityToUnlock;

    private Player m_player;

    private void Awake()
    {
        m_player = ServiceLocator.Get<Player>();
    }

    public void PickUp()
    {
        if (m_player == null || m_player.abilities == null)
        {
            return;
        }

        m_player.abilities.Enable(m_abilityToUnlock);
        Destroy(gameObject);
    }
}

