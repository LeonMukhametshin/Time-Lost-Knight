using UnityEngine;

public class FlipContoller : CoreComponent
{
    public int facingDirection { get; set; }

    private Transform m_playerTransform;

    public FlipContoller(Transform playerTransform)
    {
        m_playerTransform = playerTransform;

        facingDirection = 1;
    }

    public void CheckIfShoudFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        m_playerTransform.eulerAngles += new Vector3(0f, 180f, 0f);
    }
}