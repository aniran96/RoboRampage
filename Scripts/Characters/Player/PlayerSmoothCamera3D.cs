using Godot;

public partial class PlayerSmoothCamera3D : Camera3D
{
    // variables
    [ Export ]
    private float _speed = 44.0f;

    public override void _PhysicsProcess(double delta)
    {
        float weight = (float)Mathf.Clamp(delta * _speed, 
                        GameConstants.SMOOTH_CAMERA_CLAMP_MIN, 
                        GameConstants.SMOOTH_CAMERA_CLAMP_MAX);
        GlobalTransform = GlobalTransform.InterpolateWith( GetParent<Node3D>().GlobalTransform, weight  );
        GlobalPosition = GetParent<Node3D>().GlobalPosition;
    }
}
