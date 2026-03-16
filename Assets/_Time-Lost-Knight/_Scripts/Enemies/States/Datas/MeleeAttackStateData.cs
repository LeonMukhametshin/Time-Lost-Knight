using Game.Buffs.Interfaces;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Enemy/States/MeleeAttack")]
    [MovedFrom("")]
    public sealed class MeleeAttackStateData : ScriptableObject
    {
        [SerializeReferenceDropdown][SerializeReference] private IEffect[] m_effects;

        public IEffect[] effects => m_effects;

        [field: SerializeField] public float attackRadius { get; private set; }
        [field: SerializeField] public LayerMask playerMask { get; private set; }
    }
}
