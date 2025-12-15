using UnityEngine;

public sealed class CoinData : ScriptableObject
{
    [field: SerializeField] public int amount { get; private set; }
}