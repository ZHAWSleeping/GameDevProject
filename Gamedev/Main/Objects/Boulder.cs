using Godot;
using System;

public partial class Boulder : RigidBody2D
{
	[Export]
	public float speed;

	private Vector2 Velocity = new Vector2(0, 0);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity.X = speed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		//ForceUpdateTransform();
		MoveAndCollide((float)delta * Velocity);
	}
}
