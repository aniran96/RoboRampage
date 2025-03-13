using Godot;
using System;

public partial class Player : CharacterBody3D
{
	

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed( GameConstants.JUMP ) && IsOnFloor())
		{
			velocity.Y = GameConstants.JUMP_VELOCITY;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
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
}
