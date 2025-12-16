using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject m_sceneRootBinder;

    public void Run()
    {
        Debug.Log("Gameplay scene loaded");
    }
}