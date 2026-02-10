using UnityEngine;

[CreateAssetMenu(fileName = "StunState_Data", menuName = "Scriptable Objects/State Data/StunState_Data")]
public class StunStateData : ScriptableObject
{
    [field: SerializeField] public float stunTime { get; private set; }
    [field: SerializeField] public float stunKnockbactTime {  get; private set; }
    [field: SerializeField] public float stunKnockbackSpeed { get; private set; }   

    [field: SerializeField] public Vector2 stunKnockbackAngle { get; private set; }
}