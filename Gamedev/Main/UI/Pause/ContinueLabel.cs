using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Pause
{
	public partial class ContinueLabel : SelectableLabel
	{
		public override void Trigger()
		{
			base.Trigger();
			PersistentEvents.OnResumeRequested();
		}
	}

}