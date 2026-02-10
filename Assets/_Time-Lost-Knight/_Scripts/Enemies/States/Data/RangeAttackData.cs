using Unity.Burst;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack_Data", menuName = "Scriptable Objects/State Data/RangeAttack_Data")]
public class RangeAttackData : ScriptableObject
{
    [field: SerializeField] public GameObject projectile { get; private set; }
    [field: SerializeField][Min(0)] public float projectileDamage { get; private set; } 
    [field: SerializeField][Min(0)] public float projectileSpeed { get; private set; }  
    [field: SerializeField][Min(0)] public float projectileTrevelDistance { get; private set; }
}