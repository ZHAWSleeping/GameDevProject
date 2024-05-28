using Gamedev.Main.Tiles;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	/// <summary>
	/// Trigger for when a new room needs to be loaded.
	/// </summary>
	public partial class ZoneTrigger : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			BodyEntered += OnCameraTriggerHit;
		}

		private void OnCameraTriggerHit(Node2D node)
		{
			if (node is LoadingZone zone)
			{
				zone.TransitionCamera();
			}
		}
	}
}
