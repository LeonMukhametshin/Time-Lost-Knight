using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public int amout => m_data.amount;
    public string type => CurrencyType.COIN;

    [SerializeField] private CoinData m_data;

    private bool m_isCollected;

    public void Collect()
    {
        if (m_isCollected)
        {
            return;
        }

        m_isCollected = true;

        Destroy(gameObject);
    }
}