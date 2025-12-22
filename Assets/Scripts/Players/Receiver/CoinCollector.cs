using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private float m_pickupRadius = 2f;

    private Wallet m_wallet;

    private void Start()
    {
       m_wallet = Player.instance.context.wallet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<ICollectable>(out var collectable))
        {
            m_wallet.GetAccount(collectable.type).Add(collectable.amout);
            collectable.Collect();
        }
    }

    public void IncreasePickupRadius(float value)
    {
        m_pickupRadius += value;
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        if (TryGetComponent<CircleCollider2D>(out var collider))
        {
            collider.radius = m_pickupRadius;
        }
    }
}