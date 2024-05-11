using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace CaveGame.Main.UI.Pause
{
	public partial class RestartLabel : SelectableLabel
	{
		public override void Trigger()
		{
			base.Trigger();
			GameStateEvents.OnPlayerDied(Vector2.Zero);
		}
	}

}