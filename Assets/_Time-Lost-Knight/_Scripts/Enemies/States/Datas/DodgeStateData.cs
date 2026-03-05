using UnityEngine;

[CreateAssetMenu(fileName = "DodgeState_Data", menuName = "Scriptable Objects/State Data/DodgeState_Data")]
public class DodgeStateData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float dodgeSpeed { get; private set; }

    [field: SerializeField][Min(0)] public float dodgeCooldown { get; private set; }
    [field: SerializeField] [Min(0)] public float dodgeTime {  get; private set; }
    [field: SerializeField] public Vector2 dodgeAngle { get; private set; }
}