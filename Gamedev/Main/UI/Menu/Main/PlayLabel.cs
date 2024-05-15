using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
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
