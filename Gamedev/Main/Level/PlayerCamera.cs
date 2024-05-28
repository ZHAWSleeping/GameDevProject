using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	/// <summary>
	/// Camera that is follows the palyer through all the rooms in a level.
	/// Pos is the position of the new anchor point to which the camera is pinned.
	/// </summary>
	public partial class PlayerCamera : Camera2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			CollisionEvents.CameraTransitionTriggered += pos => GlobalPosition = pos;
		}
	}
}
