using Gamedev.Main.Events;
using Gamedev.Main.Tiles.Destructible;
using Godot;
using System;

namespace Gamedev.Main.Characters.Player
{
	public partial class DestructibleTrigger : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			BodyEntered += body =>
			{
				if (body is DestructibleMap map)
				{
					map.Break();
					CollisionEvents.OnObjectBroken();
				}
			};
		}
	}
}
