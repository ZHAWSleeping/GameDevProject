using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace CaveGame.Main.UI.Pause
{
	public partial class QuitLabel : SelectableLabel
	{
		public override void Trigger()
		{
			base.Trigger();
			StateEvents.OnQuitRequested();
		}
	}

}