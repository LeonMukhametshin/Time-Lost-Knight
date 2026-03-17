using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Particles
{
    [MovedFrom("")]
    public class AttackParticleController : MonoBehaviour
    {
        private void FinishAnim()
        {
            Destroy(gameObject);
        }
    }
}
