using Godot;
using System;

public partial class WallCheckerArea : Area2D
{
	[Export]
	CollisionShape2D wallChecker;
	public bool isOnWall = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node2D body)
	{
		
		isOnWall = true;
	}

	private void _on_body_exited()
	{

	}
}
