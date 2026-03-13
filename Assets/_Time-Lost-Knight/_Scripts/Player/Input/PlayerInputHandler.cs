using System;
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
    public bool dropDownInput { get; private set; }
    public bool interactInput { get; private set; } 

    public bool[] attackInputs { get; private set; }

    [SerializeField] private float m_inputHoldTime;

    private Camera m_camera;

    private float m_jumpInputStartTime;
    private float m_dashInputStartTime;

    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        attackInputs = new bool[count];

        m_camera = Camera.main;
    }

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

    public void OnIntarectInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            interactInput = true;
        }
        if(context.canceled)
        {
            interactInput = false;
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
        dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput.normalized);
    }

    public void OnDropDownInput(InputAction.CallbackContext contex)
    {
        if (contex.started)
        {
            dropDownInput = true;
        }
        if (contex.canceled)
        {
            dropDownInput = false;
        }
    }

    public void UseJumpInput() => 
        jumpInput = false;

    public void UseDashInput() =>
        dashInput = false;

    public void UseDropDownInput() =>
        dropDownInput = false;

    public void UseRangedAttackInput() =>
        attackInputs[(int)CombatInputs.ranged] = false;

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

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            attackInputs[(int)CombatInputs.primary] = true;
        }

        if(context.canceled)
        {
            attackInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInputs[(int)CombatInputs.secondary] = true;
        }

        if (context.canceled)
        {
            attackInputs[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnRangedAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInputs[(int)CombatInputs.ranged] = true;
        }

        if (context.canceled)
        {
            attackInputs[(int)CombatInputs.ranged] = false;
        }
    }
}