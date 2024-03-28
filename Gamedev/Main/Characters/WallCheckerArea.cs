using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class WallCheckerArea : Area2D
{
	[Export]
	CollisionShape2D wallChecker;
	public bool isOnWall = false;

	private void _on_body_entered(Node2D body)
	{
		isOnWall = true;
	}

	private void _on_body_exited(Node2D body)
	{
		isOnWall = false;
	}
}
