public class LaserProjectile : BaseProjectile 
{
    protected override bool ShouldEnableGravity() =>
        false;

    protected override void UpdateRotation() { } 
}