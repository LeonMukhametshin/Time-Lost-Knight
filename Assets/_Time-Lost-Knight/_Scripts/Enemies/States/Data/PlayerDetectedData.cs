using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetacted_Data", menuName = "Scriptable Objects/State Data/PlayerDetacted_Data")]
public class PlayerDetectedData : ScriptableObject
{
    [field: SerializeField] public float longRangeActionTime { get; private set; }
}