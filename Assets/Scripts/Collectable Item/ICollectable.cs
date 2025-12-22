public interface ICollectable
{
    int amout { get; }
    string type { get; }

    void Collect();
}