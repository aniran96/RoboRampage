using Godot;

public partial class Enemy : CharacterBody3D
{
	// node references
	[Export]
	private NavigationAgent3D _enemyNavigationAgent3DNode;
	[Export]
	private AnimationPlayer _enemyAnimPlayerNode;
	
	// constants
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	
	// variables
	private Player _player;

	[Export]
	private float _attackRange = 1.5f;
	[Export]
	private int _maxHitPoints = 100;
	
	private bool _isProvoked = false;
	private float _aggroDistance = 12.0f;
	private int _hitPoints; 

	public int HitPoints 
	{
		get {return _hitPoints;}
		set 
		{
			_hitPoints = value;
			if (_hitPoints <= 0) 
			{
				QueueFree();
			}
			_isProvoked = true;
		}
	}

    public override void _Ready()
    {
		_hitPoints = _maxHitPoints;
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

		if (_isProvoked == true && distance <= _attackRange) 
		{
			_enemyAnimPlayerNode.Play(GameConstants.ENEMY_ATTACK);
			
		}

		var direction = GlobalPosition.DirectionTo(nextPathPosition);

		if (direction != Vector3.Zero)
		{
			LookAtTarget(direction);			
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

	private void LookAtTarget( Vector3 direction ) 
	{
		Vector3 adjustedDirection = direction;
		adjustedDirection.Y = 0;
		LookAt(GlobalPosition + adjustedDirection, Vector3.Up, true);
	}

private void EnemyAttack() 
{
	GD.Print("Enemy Attack");
}

}