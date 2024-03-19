using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	public partial class WallTrigger : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			//BodyEntered += (_) => CollisionEvents.OnCollisionWall();
		}
	}
}
