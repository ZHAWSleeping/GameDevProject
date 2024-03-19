using Gamedev.Main.Tiles;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	public partial class ZoneTrigger : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			BodyEntered += OnCameraTriggerHit;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

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
