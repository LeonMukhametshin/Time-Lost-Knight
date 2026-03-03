using UnityEngine;

public class AbilityUnlockPickup : MonoBehaviour
{
    [SerializeField] private PlayerAbilityType m_abilityToUnlock = PlayerAbilityType.WallGrab;
    [SerializeField] private bool m_destroyOnPickup = true;

    private Player m_player;

    private void Awake()
    {
        m_player = ServiceLocator.Get<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (m_player == null || m_player.abilities == null)
        {
            return;
        }

        m_player.abilities.Enable(m_abilityToUnlock);

        if (m_destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}

