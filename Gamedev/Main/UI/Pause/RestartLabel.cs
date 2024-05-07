using Gamedev.Events;
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
			StateEvents.OnRestartRequested(Vector2.Zero);
		}
	}

}