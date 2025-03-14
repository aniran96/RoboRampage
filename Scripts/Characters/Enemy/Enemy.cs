using Godot;

public partial class Enemy : CharacterBody3D
{
	// node references
	[Export]
	private NavigationAgent3D _enemyNavigationAgent3DNode;
	
	
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private Player _player;

    public override void _Ready()
    {
        _player = (Player)GetTree().GetFirstNodeInGroup( nameof(Player) );
    }

    public override void _Process(double _delta)
    {
        _enemyNavigationAgent3DNode.TargetPosition = _player.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
	{
		var nextPathPosition = _enemyNavigationAgent3DNode.GetNextPathPosition();
		Vector3 velocity = Velocity;

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
