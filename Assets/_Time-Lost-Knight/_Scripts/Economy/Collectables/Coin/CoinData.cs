using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Scriptable Objects/CoinData")]
public sealed class CoinData : ScriptableObject
{
    [field: SerializeField] [Min(0)] public int amount { get; private set; }
}