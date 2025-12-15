using UnityEngine;

public class CollectableTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collectable = collision.GetComponent<ICollectable>();

        if (collectable != null)
        {
            collectable.Collect();
            Debug.Log($"Collect {collision.gameObject.name}");
        }
    }
}