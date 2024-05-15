using Gamedev.Main.Events;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class PlayLabel : SelectableLabel
{
	public override void Trigger()
	{
		base.Trigger();
		PersistentEvents.OnFileSelectOpened();
	}
}
