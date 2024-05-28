using Gamedev.Main.Characters;
using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	/// <summary>
	/// Trigger for when the palyer comes into contact with a deadly wall/spike/floor.
	/// </summary>
	public partial class DeathTrigger : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			BodyEntered += (_) => CollisionEvents.OnCollisionDeath();
		}
	}
}