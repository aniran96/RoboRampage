using Godot;

public partial class Enemy : CharacterBody3D
{
	// node references
	[Export]
	private NavigationAgent3D _enemyNavigationAgent3DNode;
	
	// constants
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	
	// variables
	private Player _player;

	private bool _isProvoked = false;
	private float _aggroDistance = 12.0f;

    public override void _Ready()
    {
        _player = (Player)GetTree().GetFirstNodeInGroup( nameof(Player) );
    }

    public override void _Process(double _delta)
    {
		if (_isProvoked == true) 
		{ 
			_enemyNavigationAgent3DNode.TargetPosition = _player.GlobalPosition; 
		}

    }

    public override void _PhysicsProcess(double delta)
	{
		var nextPathPosition = _enemyNavigationAgent3DNode.GetNextPathPosition();
		Vector3 velocity = Velocity;

		var distance = GlobalPosition.DistanceTo(_player.GlobalPosition);

		if (distance <= _aggroDistance) 
		{ 
			_isProvoked = true; 
		}

		var direction = GlobalPosition.DirectionTo(nextPathPosition);

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
