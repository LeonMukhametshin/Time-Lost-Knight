using UnityEngine;

[CreateAssetMenu(fileName = "DeadState_Data", menuName = "Scriptable Objects/State Data/DeadState_Data")]
public class DeadStateData : ScriptableObject
{
    [field: SerializeField] public GameObject deathChunkParticle;
    [field: SerializeField] public GameObject deathBloodParticle;
}