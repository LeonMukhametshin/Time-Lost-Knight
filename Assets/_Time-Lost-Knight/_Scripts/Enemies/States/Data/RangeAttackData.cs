using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Scriptable Objects/Enemy/States/RangeAttack")]
public class RangeAttackData : ScriptableObject
{
    [SerializeReferenceDropdown][SerializeReference] private IEffect[] m_effects;

    public IEffect[] effects => m_effects;

    [field: SerializeField] public GameObject projectile { get; private set; }
    [field: SerializeField][Min(0)] public float travelDistance { get; private set; }
    [field: SerializeField][Min(0)] public float speed { get; private set; }
}