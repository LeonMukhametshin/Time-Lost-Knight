using UnityEngine;

[CreateAssetMenu(fileName = "PatrolBehaviourData", menuName = "Scriptable Objects/Enemy/Behaviour/PatrolBehaviourData")]
public class PatrolBehaviourData : EnemyBehaviuorData
{
    [field: SerializeField][Min(0)] public float rayLenght { get; private set; }
}