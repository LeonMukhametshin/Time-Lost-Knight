using System;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Camera
{
    [Serializable]
    [MovedFrom("")]
    public class CameraContext
    {
        public int facing;
        public bool isMoving;

        public bool isLocked;
        public string lockReason;

        public float timeSinceLastMove;
    }
}
