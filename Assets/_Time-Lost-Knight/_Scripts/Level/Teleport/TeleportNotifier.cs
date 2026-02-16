using System;
using UnityEngine;

[Serializable]
public class TeleportNotifier
{
    public event Action<GameObject, Vector2> Teleported;

    public void Notify(GameObject subject, Vector2 position)
    {
        subject.SendMessage("OnTeleported", position, SendMessageOptions.DontRequireReceiver);
        Teleported?.Invoke(subject, position);
    }
}
