using Godot;


public class GameConstants
{
    public  const float SPEED = 5.0f;
	//public  const float JUMP_VELOCITY = 4.5f;
    public const float SMOOTH_CAMERA_CLAMP_MIN = 0.0F;
    public const float SMOOTH_CAMERA_CLAMP_MAX = 0.1F;
    public const string CAMERA_PIVOT = "CameraPivot";
    public const int CAMERA_ROTATION_LIMIT = 90;
    public const string COOLDOWNTIMER = "CoolDownTimer";
    
    public static readonly StringName INPUT_LEFT = "move_left";
    public static readonly StringName INPUT_FRONT = "move_front";
    public static readonly StringName INPUT_RIGHT = "move_right";
    public static readonly StringName INPUT_BACK = "move_back";
    public static readonly StringName JUMP = "jump";
    public static readonly StringName FREE_CAMERA = "free_camera";
    public static readonly StringName ENEMY_ATTACK = "Attack";
    public static readonly StringName SHOOT = "shoot";
    
}
