using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class LevelSelectLabel : SelectableLabel
{
	[Export]
	private PackedScene levelScene;

	public override void Trigger()
	{
		GD.Print("Level selected");
		base.Trigger();
		PersistentEvents.OnSceneChangeRequested(levelScene);
	}
}
