using System;
using Godot;

public partial class Player : CharacterBody3D
{
	// node references
	private Node3D _cameraPivotNode;

	// variables
	[ Export ]
	private float _mouseSensitivity = 0.001f;
	[Export]
	private float _jumpHeight = 1.0f;
	[Export]
	private float _fallMultiplier = 2.5f;
	[Export]
	private int _maxHitPoints = 100;

	private int _hitPoints;

	public int HitPoints 
	{
		get { return _hitPoints;}
		set 
		{
			_hitPoints = value;
			if ( _hitPoints <= 0 ) 
			{
				GetTree().Quit();
			}
		}
	}

	private Vector2 _mouseMotion = Vector2.Zero;
	
    public override void _Ready()
    {
		_hitPoints = _maxHitPoints;
		AddToGroup( nameof(Player) );
		_cameraPivotNode = GetNode<Node3D>( GameConstants.CAMERA_PIVOT );
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }


    public override void _PhysicsProcess(double delta)
	{
		HandleCameraRotation();
		Vector3 velocity = Velocity;
		var gravity = GetGravity();
		if (!IsOnFloor())
		{
			if ( velocity.Y >= 0 ) 
			{
				velocity.Y += gravity.Y * (float)delta;
			}
			else 
			{
				velocity.Y += gravity.Y * (float)delta * _fallMultiplier;
			}
		}
		if (Input.IsActionJustPressed( GameConstants.JUMP ) && IsOnFloor())
		{
			velocity.Y = (float)Math.Sqrt(_jumpHeight * 2.0 * -gravity.Y);
		}
		Vector2 inputDir = Input.GetVector(GameConstants.INPUT_LEFT, GameConstants.INPUT_RIGHT, GameConstants.INPUT_FRONT, GameConstants.INPUT_BACK);
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * GameConstants.SPEED;
			velocity.Z = direction.Z * GameConstants.SPEED;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, GameConstants.SPEED);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, GameConstants.SPEED);
		}
		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Input( InputEvent @event ) 
	{
		if ( @event is InputEventKey inputEventKey ) 
		{
			if ( inputEventKey.IsActionPressed( GameConstants.FREE_CAMERA ) )
			{
				Input.MouseMode = Input.MouseModeEnum.Visible;
			} 
		}
		if ( Input.MouseMode == Input.MouseModeEnum.Captured ) 
		{

			if ( @event is InputEventMouseMotion mouseMotionEvent ) 
			{
				_mouseMotion = -mouseMotionEvent.Relative * _mouseSensitivity;
			}
		}
	}

	private void HandleCameraRotation() 
	{
		RotateY( _mouseMotion.X );
		_cameraPivotNode.RotateX( _mouseMotion.Y );
		var xRotation = Mathf.Clamp( _cameraPivotNode.RotationDegrees.X, 
									-GameConstants.CAMERA_ROTATION_LIMIT, 
									GameConstants.CAMERA_ROTATION_LIMIT );
		_cameraPivotNode.RotationDegrees = new Vector3( xRotation, _cameraPivotNode.RotationDegrees.Y, _cameraPivotNode.RotationDegrees.Z ); 
		_mouseMotion = Vector2.Zero;
	}
}
