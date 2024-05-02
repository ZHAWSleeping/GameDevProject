using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	public partial class PlayerCamera : Camera2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			CollisionEvents.CameraTransitionTriggered += pos => GlobalPosition = pos;
		}
	}
}
