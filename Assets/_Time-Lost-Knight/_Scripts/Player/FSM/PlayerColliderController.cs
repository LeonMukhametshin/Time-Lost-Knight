using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [field:SerializeField] public BoxCollider2D movementCollider { get; private set; }

    private Vector2 m_workspace;

    public void SetColliderHeight(float height)
    {
        Vector2 center = movementCollider.offset;
        m_workspace.Set(movementCollider.size.x, height);

        center.y += (height - movementCollider.size.y) / 2;

        movementCollider.size = m_workspace;
        movementCollider.offset = center;
    }
}