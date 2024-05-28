using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.UI.Buttons
{
	public partial class RestartButton : Button
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			ButtonUp += () =>
			{
				GameStateEvents.OnPlayerDied(Vector2.Zero);
				PersistentEvents.OnPlayerDied(Vector2.Zero);
			};
		}
	}
}