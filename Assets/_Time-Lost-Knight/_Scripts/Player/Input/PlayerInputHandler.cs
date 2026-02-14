using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public Vector2 rawDashDirectionInput { get; private set; }
    public Vector2Int dashDirectionInput { get; private set; }

    public int normalizedInputX { get; private set; }    
    public int normalizedInputY { get; private set; }

    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool grabInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool dashInputStop { get; private set; }

    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private Camera m_camera;

    [SerializeField] private float m_inputHoldTime;

    private float m_jumpInputStartTime;
    private float m_dashInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashIputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();

        //TODO: different dash 
        normalizedInputX = Mathf.RoundToInt(rawMovementInput.x);
        normalizedInputY = Mathf.RoundToInt(rawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            jumpInput = true;
            jumpInputStop = false;
            m_jumpInputStartTime = Time.time;
        }
        if(context.canceled)
        {
            jumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            grabInput = true;
        }
        if(context.canceled)
        {
            grabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            dashInput = true;
            dashInputStop = false;
            m_dashInputStartTime = Time.time;
        }
        else if(context.canceled)
        {
            dashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        rawDashDirectionInput = context.ReadValue<Vector2>();

        if (m_playerInput.currentControlScheme == "Keyboard")
        {
            rawDashDirectionInput = m_camera.ScreenToWorldPoint((Vector3)rawDashDirectionInput - transform.position);
        }

        dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput.normalized);
    }

    public void UseJumpInput() => 
        jumpInput = false;

    public void UseDashInput() =>
        dashInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= m_jumpInputStartTime + m_inputHoldTime)
        {
            jumpInput = false;
        }
    }

    private void CheckDashIputHoldTime()
    {
        if(Time.time >= m_dashInputStartTime + m_inputHoldTime)
        {
            dashInput = false;
        }
    }
}