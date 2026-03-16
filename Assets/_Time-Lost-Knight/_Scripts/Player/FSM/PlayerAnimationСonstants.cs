using Game.Core.CoreComponents;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM
{
    [MovedFrom("")]
    public class PlayerAnimationConstants
    {
        public const string IDLE = "idle";
        public const string MOVEMENT = "movement";
        public const string IN_AIR = "inAir";
        public const string LAND = "land";

        public const string Y_VELOCITY = "yVelocity";
        public const string X_VELOCITY = "xVelocity";

        public const string WALL_SLIDE = "wallSlide";
        public const string WALL_GRAB = "wallGrab";
        public const string WALL_CLIMB = "wallClimb";

        public const string LEDGE_CLIMB = "ledgeClimb";
        public const string LEDGE_CLIMB_STATE = "ledgeClimbState";
        public const string CROUCH_IDLE = "crouchIdle";
        public const string CROUCH_MOVE = "crouchMove";
        public const string IS_TOUCHING_CEILING = "isTouchingCeiling";
        public const string ATTACK = "attack";
        public const string RANGED_ATTACK = "rangedAttack";
    }
}
