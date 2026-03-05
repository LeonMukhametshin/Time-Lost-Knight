using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Scriptable Objects/Enemy/States/RangeAttack")]
public class RangeAttackData : ScriptableObject
{
    [field: SerializeField] public GameObject projectile { get; private set; }
    [field: SerializeField][Min(0)] public float travelDistance { get; private set; }
    [field: SerializeField][Min(0)] public float speed { get; private set; }
}