using NUnit.Framework.Constraints;
using UnityEngine;

public class FlipContoller : MonoBehaviour
{
    private PlayerCollisionDetector m_collisionDetector;

    public void Initialize(PlayerCollisionDetector collisionDetector) =>
         m_collisionDetector = collisionDetector;

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
        transform.eulerAngles += new Vector3(0f, 180f, 0f);
    }
}