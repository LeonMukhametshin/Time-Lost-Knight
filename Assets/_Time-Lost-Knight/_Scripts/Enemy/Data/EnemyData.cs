using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public sealed class EnemyData : ScriptableObject
{
    [field: SerializeField] public int maxHealt { get; private set; }
    [field: SerializeField] public EnemyBehaviuorData enemyBehaviuorData { get; private set ;}
}