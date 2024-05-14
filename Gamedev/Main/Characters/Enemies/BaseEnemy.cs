using Gamedev.Main.UI.Debug;
using Godot;
using System;

public partial class BaseEnemy : CharacterBody2D
{
	[Export]
	public float speed;
	[Export]
	public RayCastEnemy FloorWallPlayerDetection;
	[Export]
	public TopDetectionEnemy topDetectionEnemyRight;
	[Export]
	public TopDetectionEnemy topDetectionEnemyLeft;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity = new(speed, 0);
		topDetectionEnemyRight.PlayerJumpedOnTop += Die;
		topDetectionEnemyLeft.PlayerJumpedOnTop += Die;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = new Vector2(0, 0);

		if (FloorWallPlayerDetection.NoFloor())
		{
			velocity.Y += 16;
			Velocity += velocity;
			MoveAndSlide();
			return;
		}
		if (FloorWallPlayerDetection.NoGap() && FloorWallPlayerDetection.NoWall())
		{
			MoveAndSlide();
			return;
		}
		else if (!FloorWallPlayerDetection.groundBack || FloorWallPlayerDetection.wallLeft)
		{
			velocity.X = speed;
		}
		else
		{
			velocity.X = -speed;
		}

		Velocity = velocity;

		MoveAndSlide();
	}


	protected virtual void Die()
	{
		QueueFree();
	}
}
