using Gamedev.Main.Events;
using Godot;
using System;


namespace Gamedev.Main.Objects
{
	/// <summary>
	/// The goal for each level.
	/// Put at the end of a level.
	/// </summary>
	public partial class Goal : Area2D
	{
		public override void _Ready()
		{
			BodyEntered += _ => CollisionEvents.OnGoalReached();
		}
	}
}
