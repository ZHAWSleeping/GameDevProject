using Gamedev.Events;
using Gamedev.Main;
using Gamedev.Main.Events;
using Godot;
using System;
using System.Collections.Generic;

namespace Gamedev.Main.Tiles
{
	public partial class LoadingZone : TileMapLayer
	{
		[Export]
		private Node2D Anchor;

		public int Id = -1;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Color modulate = Modulate;
			modulate.A = 0;
			Modulate = modulate;
		}

		public void TransitionCamera()
		{
			CollisionEvents.OnCameraTransitionTriggered(Anchor.GlobalPosition);
			StateEvents.OnRoomChanged(Id);
		}
	}
}
