using Godot;


public partial class HitScanWeapon : Node3D
{
	// scene references
	[Export] 
	private PackedScene _sparksScene;
	// node references
	private Timer _coolDownTimerNode;

	[Export]
	private RayCast3D _weaponRayCast3DNode;

	[Export]
	private Node3D _weaponMesh;
	[Export]
	private GpuParticles3D _muzzleFlash;


	//variables
	[Export]
	private float _fireRate;
	[Export]
	private float _recoilDist;
	[Export]
	private float _lerpFactor = 10.0f;
	[Export]
	private float _rayCastDist = 100.0f;
	[Export]
	private int _weaponDamage = 15;

	private Vector3 _weaponStartPos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_weaponStartPos = _weaponMesh.Position;
		_coolDownTimerNode = GetNode<Timer>(GameConstants.COOLDOWNTIMER);
		
	//	_weaponRayCast3DNode.Position = _weaponMesh.Position;
		_weaponRayCast3DNode.TargetPosition = new Vector3(_weaponRayCast3DNode.Position.X, _weaponRayCast3DNode.Position.Y, -_rayCastDist);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed(GameConstants.SHOOT)) 
		{
			if (_coolDownTimerNode.IsStopped()) 
			{
				Shoot();
			}
		}
		_weaponMesh.Position = _weaponMesh.Position.Lerp( _weaponStartPos, (float)delta * _lerpFactor);
	}

	private void Shoot() 
	{
		_muzzleFlash.Restart();
		var _weaponMeshPosition = _weaponMesh.Position;
		_coolDownTimerNode.Start(1.0 / _fireRate);
		_weaponMeshPosition.Z += _recoilDist;
		_weaponMesh.Position = _weaponMeshPosition;
		var collider = _weaponRayCast3DNode.GetCollider();
		GD.Print(collider);
		if ( collider != null && (collider.IsClass( nameof( CharacterBody3D ) ) ) )
		{
			Enemy enemy = (Enemy)collider;
			enemy.HitPoints -= _weaponDamage;	
		}
		var sparks = _sparksScene.Instantiate<GpuParticles3D>();
		AddChild(sparks);
		sparks.GlobalPosition = _weaponRayCast3DNode.GetCollisionPoint();
	}
}
				
