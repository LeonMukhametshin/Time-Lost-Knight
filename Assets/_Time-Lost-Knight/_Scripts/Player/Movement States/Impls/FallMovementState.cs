using Inputs;
using UnityEngine;

public class FallMovementState : AirborneMovementState
{
    public FallMovementState(MovementStateMachine fsm, PlayerInputController inputs,
        Rigidbody2D rigidbody)
        : base(fsm, rigidbody, inputs)
    {
    }
    //TODO fall logic
}