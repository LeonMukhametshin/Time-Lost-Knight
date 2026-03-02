using UnityEngine;

public class FlipContoller : CoreComponent
{
    [SerializeField] private Transform m_entityTransform;
    public int facingDirection { get; set; } = 1;

    public void CheckIfShoudFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        m_entityTransform.eulerAngles += new Vector3(0f, 180f, 0f);
    }
}