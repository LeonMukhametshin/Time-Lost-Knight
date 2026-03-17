using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "PlayerDetacted", menuName = "Scriptable Objects/Enemy/States/PlayerDetacted")]
    [MovedFrom("")]
    public sealed class PlayerDetectedData : ScriptableObject
    {
        [field: SerializeField] public float longRangeActionTime { get; private set; }
    }
}
