using UnityEngine;

public class ColliderController : CoreComponent
{
    [SerializeField] private BoxCollider2D colider;

    private Vector2 m_workspace;

    public void SetColliderHeight(float height)
    {
        Vector2 center = colider.offset;
        m_workspace.Set(colider.size.x, height);

        center.y += (height - colider.size.y) / 2;

        colider.size = m_workspace;
        colider.offset = center;
    }
}