using UnityEngine;

[CreateAssetMenu(fileName = "Entity_Data", menuName = "Scriptable Objects/Entity_Data")]
public class EntityData : ScriptableObject
{
    [field: SerializeField] public float damageHopSpeed { get; private set; }   

    [field: SerializeField][Min(0)] public float stunResistance { get; private set; }
    [field: SerializeField][Min(0)] public float stunRecoveryTime { get; private set; }
}