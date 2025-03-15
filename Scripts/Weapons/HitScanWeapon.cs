using Godot;

public partial class HitScanWeapon : Node3D
{
	// node references
	private Timer _coolDownTimerNode;

	[Export]
	private Node3D _weaponMesh;

	//variables
	[Export]
	private float _fireRate;
	[Export]
	private float _recoilDist;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coolDownTimerNode = GetNode<Timer>(GameConstants.COOLDOWNTIMER);
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
	}

	private void Shoot() 
	{
		var _weaponMeshPosition = _weaponMesh.Position;
		_coolDownTimerNode.Start(1.0 / _fireRate);
		_weaponMeshPosition.Z += _recoilDist;
		_weaponMesh.Position = _weaponMeshPosition;

		GD.Print("Weapon Fired");
	}
}
				
