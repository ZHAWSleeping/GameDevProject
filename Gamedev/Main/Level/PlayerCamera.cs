using Godot;
using System;

namespace Gamedev.Main.Characters
{
	public partial class PlayerCamera : Camera2D
	{
		[Export]
		private Node2D anchor;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (anchor is not null)
			{
				Vector2 position = GlobalPosition;
				position.X = anchor.GlobalPosition.X;
				GlobalPosition = position;
			}
		}
	}
}
