public interface IPlayerInput
{
    float GetHorizontalInput();
    bool IsJumpPressed();
    bool IsJumpHeld();
    bool IsDashPressed();
    void ClearJumpInput();
}