using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private CoinData m_data;

    public event Action сollected;
    public event Action<int> сollectedValue;

    private bool m_isCollected;

    public bool TryCollect()
    {
        if(m_isCollected) return false;

        m_isCollected = true;
        сollected?.Invoke();
        сollectedValue?.Invoke(m_data.amount);

        Collected();

        return true;
    }

    private void Collected()
    {
        // TODO Animation 
        Destroy(gameObject);
    }
}