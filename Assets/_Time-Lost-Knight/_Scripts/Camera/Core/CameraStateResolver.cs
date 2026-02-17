public sealed class CameraStateResolver
{
    private readonly float m_recenterDelay;

    public CameraStateResolver(float recenterDelay)
    {
        m_recenterDelay = recenterDelay;
    }

    public CameraMode Resolve(CameraContext context)
    {
        if (context.isLocked)
            return CameraMode.Locked;

        if (!context.isMoving && context.timeSinceLastMove >= m_recenterDelay)
            return CameraMode.Center;

        return context.facing > 0 ? CameraMode.LeftThird : CameraMode.RightThird;
    }
}
