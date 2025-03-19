using Godot;
using System;
using System.Collections.Generic;

public partial class AmmoHandler : Node
{
	//variables
	private Dictionary<AmmoType, int> _ammoStorage = new Dictionary<AmmoType, int>
	{
		{ AmmoType.BULLET, 10 },
		{ AmmoType.SMALL_BULLET, 30 }
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
