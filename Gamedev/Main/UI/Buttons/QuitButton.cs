using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.UI.Buttons
{
	public partial class QuitButton : Button
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			ButtonUp += PersistentEvents.OnQuitRequested;
		}
	}
}