using Game.Observer;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Teleport
{
    [Serializable]
    [MovedFrom("")]
    public class TeleportNotifier
    {
        public event Action<GameObject, Vector2> Teleported;

        public void Notify(GameObject subject, Vector2 position)
        {
            subject.SendMessage("OnTeleported", position, SendMessageOptions.DontRequireReceiver);
            Teleported?.Invoke(subject, position);
        }
    }
}
