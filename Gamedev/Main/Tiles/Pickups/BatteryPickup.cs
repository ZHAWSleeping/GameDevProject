using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Tiles.Pickups
{
	/// <summary>
	/// Little item that can be picked up by touching it.
	/// Used to light up gems.
	/// </summary>
	public partial class BatteryPickup : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		private void _on_body_entered(Node2D body)
		{
			CollisionEvents.OnBatteryCollected();
			PersistentEvents.OnBatteryCollected();
			QueueFree();
		}
	}
}