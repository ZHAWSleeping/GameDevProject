using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class WallCheckerArea : Area2D
{
	[Export]
	CollisionShape2D wallChecker;
	public bool isOnWall = false;
	public Vector2 currentWallPosition;

	private void _on_body_entered(Node2D body)
	{
		isOnWall = true;
		currentWallPosition = body.GlobalPosition;
		GD.Print("something entered" + body.GetType());
	}

	private void _on_body_exited(Node2D body)
	{
		isOnWall = false;
		currentWallPosition = new Vector2(-99, -99); // for error finding maybe bad idea
		GD.Print("something exited" + body.GetType());
	}
}
