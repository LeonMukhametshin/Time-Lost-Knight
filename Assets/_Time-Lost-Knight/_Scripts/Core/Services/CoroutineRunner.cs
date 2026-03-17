using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.Services
{
    [MovedFrom("")]
    public sealed class CoroutineRunner : MonoBehaviour
    {
        public void Awake() =>
            DontDestroyOnLoad(this);
    }
}
