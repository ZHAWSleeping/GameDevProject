using Gamedev.Main.Events;
using Godot;
using System;


namespace Gamedev.Main.Objects
{
	public partial class Goal : Area2D
	{
		public override void _Ready()
		{
			BodyEntered += _ => CollisionEvents.OnGoalReached();
		}
	}
}
