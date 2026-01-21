using System.Collections;
using UnityEngine;

public sealed class CoroutineRunner : MonoBehaviour 
{
    public Coroutine Run(IEnumerator routine) =>
        StartCoroutine(routine);

    public void Stop(Coroutine coroutine)
    {
        if(coroutine is not null)
        {
            StopCoroutine(coroutine);
        }
    }
}