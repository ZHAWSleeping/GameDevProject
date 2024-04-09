using Gamedev.Main.Characters;
using Gamedev.Main.Events;
using Godot;
using System;

public partial class EnemyTrigger : Area2D
{
	public override void _Ready()
	{
		BodyEntered += (_) => CollisionEvents.OnCollisionDeath();
	}
}
