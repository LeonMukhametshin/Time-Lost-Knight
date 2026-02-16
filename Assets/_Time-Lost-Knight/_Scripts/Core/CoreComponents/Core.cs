public class Core 
{
    public readonly Movement movement;
    public readonly FlipContoller flipController;
    public readonly CollisionDetector collisionDetector;

    public Core(Movement movement, FlipContoller flipContoller, CollisionDetector collisionDetector)
    {
        this.movement = movement;
        this.flipController = flipContoller;
        this.collisionDetector = collisionDetector;
    }

    public void Update()
    {
        movement.Update();
    }
}