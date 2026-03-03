using UnityEngine;


[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Enemy/States/MeleeAttack")]
public sealed class MeleeAttackStateData : ScriptableObject
{
    [SerializeReferenceDropdown][SerializeReference] private IEffect[] m_effects;

    public IEffect[] effects => m_effects;

    [field: SerializeField] public float attackRadius { get; private set; }
    [field: SerializeField] public LayerMask playerMask { get; private set; }
}