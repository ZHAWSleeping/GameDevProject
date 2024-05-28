using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Tiles.Pickups.PowerUps
{
	public partial class WallJumpPowerUp : Area2D
	{
		[Export]
		CollisionShape2D collisionShape2D;

		private void _on_body_entered(Node2D body)
		{
			QueueFree();
		}

	}
}