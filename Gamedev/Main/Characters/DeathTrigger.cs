using Gamedev.Main.Characters;
using Gamedev.Main.Events;
using Godot;
using System;

public partial class DeathTrigger : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += (_) =>  CollisionEvents.OnCollisionDeath();
	}
}
