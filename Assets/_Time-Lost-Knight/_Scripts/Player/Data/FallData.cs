using UnityEngine;

[CreateAssetMenu(fileName = "FallData", menuName = "Scriptable Objects/Player/FallData")]
public class FallData : ScriptableObject
{
    [field: SerializeField] public AnimationCurve fallAccelerationCurve { get; private set; }
    [field: SerializeField][Min(0)] public float fallAcceleration { get; private set; }
    [field: SerializeField][Min(0)] public float fallAccelerationDuration { get; private set; }
    [field: SerializeField][Min(0)] public float maxFallSpeed { get; private set; }
}