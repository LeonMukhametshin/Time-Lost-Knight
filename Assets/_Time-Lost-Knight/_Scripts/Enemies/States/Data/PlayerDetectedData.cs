using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetacted", menuName = "Scriptable Objects/Enemy/States/PlayerDetacted")]
public sealed class PlayerDetectedData : ScriptableObject
{
    [field: SerializeField] public float longRangeActionTime { get; private set; }
}