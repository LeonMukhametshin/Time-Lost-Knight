using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    private CoinData m_data;

    public event Action collect;

    public void Initialize(CoinData data)
    {
        m_data = data;
    }

    public void Collect()
    {
        collect?.Invoke();

        Collected();
    }

    private void Collected()
    {
        Destroy(gameObject);
    }
}