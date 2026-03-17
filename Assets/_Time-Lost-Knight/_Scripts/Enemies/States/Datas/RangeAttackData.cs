using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "RangeAttack", menuName = "Scriptable Objects/Enemy/States/RangeAttack")]
    [MovedFrom("")]
    public class RangeAttackData : ScriptableObject
    {
        [field: SerializeField] public GameObject projectile { get; private set; }
        [field: SerializeField][Min(0)] public float speed { get; private set; }
    }
}
