using UnityEngine;

public class FlipContoller 
{
    private Transform m_playerTransform;
    private CollisionDetector m_collisionDetector;

    public FlipContoller(Transform playerTransform, CollisionDetector collisionDetector)
    {
        m_playerTransform = playerTransform;
        m_collisionDetector = collisionDetector;
    }

    public void CheckIfShoudFlip(int xInput)
    {
        if (xInput != 0 && xInput != m_collisionDetector.facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_collisionDetector.facingDirection *= -1;
        m_playerTransform.eulerAngles += new Vector3(0f, 180f, 0f);
    }
}