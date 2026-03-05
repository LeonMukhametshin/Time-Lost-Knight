using UnityEngine;

[CreateAssetMenu(fileName = "LookForPlayer", menuName = "Scriptable Objects/Enemy/States/LookForPlayer")]
public sealed class LookForPlayerStateData : ScriptableObject
{
    [field: SerializeField] public int amountOfTurns { get; private set; }
    [field: SerializeField] public float timeBetweenTurns { get; private set; }
}