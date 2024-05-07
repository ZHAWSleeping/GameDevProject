using Gamedev.Events;
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
		StateEvents.OnSceneChangeRequested(levelScene);
	}
}
