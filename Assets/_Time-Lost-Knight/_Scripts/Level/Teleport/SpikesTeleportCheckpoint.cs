using Game.Traps;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Teleport
{
    [MovedFrom("")]
    public class SpikesTeleportCheckpoint : MonoBehaviour
    {
        [SerializeField] private StaticTeleportSpike[] m_spikes;
        [SerializeField] private Transform m_checkpointPoint;

        public void SetNewTeleportPoint()
        {
            if (m_checkpointPoint == null)
            {
                return;
            }

            foreach (var spikes in m_spikes)
            {
                if (spikes == null)
                {
                    continue;
                }

                spikes.SetTeleportPoint(m_checkpointPoint.position);
            }
        }
    }
}
