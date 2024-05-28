using Godot;
using System;

namespace Gamedev.Main.Objects
{
	/// <summary>
	/// A rolling boulder.
	/// Speed and direction can be set in the editor.
	/// </summary>
	public partial class Boulder : RigidBody2D
	{
		[Export]
		public float speed;

		private Vector2 Velocity = new Vector2(0, 0);


		public override void _Ready()
		{
			Velocity.X = speed;
		}

		public override void _PhysicsProcess(double delta)
		{
			MoveAndCollide((float)delta * Velocity);
		}
	}
}