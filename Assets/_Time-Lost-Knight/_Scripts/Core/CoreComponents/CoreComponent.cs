using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.CoreComponents
{
    [MovedFrom("")]
    public class CoreComponent : MonoBehaviour
    {
        [SerializeField] protected CoreSystem core;

        public virtual void Awake()
        {
            //core.AddCoreComponent(this);
        }
    }
}
