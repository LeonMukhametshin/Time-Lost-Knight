using UnityEngine;

[CreateAssetMenu(fileName = "Entity_Data", menuName = "Scriptable Objects/Entity_Data")]
public class EntityData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float maxHealth { get; private set; }

    [field: SerializeField] public float damageHopSpeed { get; private set; }   

    [field: SerializeField][Min(0)] public float wallCheckDistance { get; private set; }
    [field: SerializeField][Min(0)] public float ledgeCheckDistance { get; private set; }
    [field: SerializeField][Min(0)] public float groundCheckRadius { get; private set; }

    [field: SerializeField][Min(0)] public float maxAgroDistance { get; private set; }
    [field: SerializeField][Min(0)] public float minAgroDistance { get; private set; }

    [field: SerializeField][Min(0)] public float stunResistance { get; private set; }
    [field: SerializeField][Min(0)] public float stunRecoveryTime { get; private set; }

    [field: SerializeField][Min(0)] public float closeRangeActionDistance { get; private set; }

    [field: SerializeField] public GameObject hitParticle;

    [field: SerializeField] public LayerMask groundLayer { get; private set; }
    [field: SerializeField] public LayerMask playerLayer { get; private set; }
}