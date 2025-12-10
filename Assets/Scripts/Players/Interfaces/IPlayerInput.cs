public interface IPlayerInput
{
    float GetHorizontalInput();
    bool IsJumpPressed();
    bool IsJumpHeld();
    void ClearJumpInput();
}