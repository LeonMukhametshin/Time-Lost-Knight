using UnityEngine;

public sealed class CoroutineRunner : MonoBehaviour 
{
    public void Awake() =>
        DontDestroyOnLoad(this);
}