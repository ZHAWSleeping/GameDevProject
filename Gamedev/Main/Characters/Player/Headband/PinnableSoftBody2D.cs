using Godot;
using System;

public partial class PinnableSoftBody2d : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Name == "Bone-0" || Name == "Bone-1") {
			Freeze = true;
		}
	}
}
