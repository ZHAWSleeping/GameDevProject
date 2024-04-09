using Gamedev.Main.UI.Debug;
using Godot;
using System;

public partial class BaseEnemy : RigidBody2D
{
	[Export]
	public float speed;
	[Export]
	public RayCastEnemy terrainDetection;

	private Vector2 Velocity = new Vector2(0, 0);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity.X = speed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = new Vector2(0, 0);
		if (terrainDetection.NoGap() && terrainDetection.NoWall())
		{
			Move(delta);
			return;
		}
		else if (!terrainDetection.groundBack || terrainDetection.groundLeft)
		{
			velocity.X = speed;
		}
		else
		{
			velocity.X = -speed;
		}

		Velocity = velocity;

		Move(delta);
	}

	private void Move(double delta)
	{
		MoveAndCollide((float)delta * Velocity);
	}
}
