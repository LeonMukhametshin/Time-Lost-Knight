using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "LookForPlayer", menuName = "Scriptable Objects/Enemy/States/LookForPlayer")]
    [MovedFrom("")]
    public sealed class LookForPlayerStateData : ScriptableObject
    {
        [field: SerializeField] public int amountOfTurns { get; private set; }
        [field: SerializeField] public float timeBetweenTurns { get; private set; }
    }
}
