using UnityEngine;

public class ColliderController
{
    private BoxCollider2D colider;

    private Vector2 m_workspace;

    public ColliderController(BoxCollider2D colder)
    {
        this.colider = colder;
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = colider.offset;
        m_workspace.Set(colider.size.x, height);

        center.y += (height - colider.size.y) / 2;

        colider.size = m_workspace;
        colider.offset = center;
    }
}