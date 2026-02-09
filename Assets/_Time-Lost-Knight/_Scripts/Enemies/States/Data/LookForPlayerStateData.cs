using UnityEngine;

[CreateAssetMenu(fileName = "LookForPlayerState_Data", menuName = "Scriptable Objects/State Data/LookForPlayerState_Data")]
public class LookForPlayerStateData : ScriptableObject
{
    [field: SerializeField] public int amountOfTurns { get; private set; }
    [field: SerializeField] public float timeBetweenTurns { get; private set; }
}