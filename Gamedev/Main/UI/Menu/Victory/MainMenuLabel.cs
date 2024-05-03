using Gamedev.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class MainMenuLabel : SelectableLabel
{
	[Export]
	private PackedScene mainMenuScene;
	public override void Trigger()
	{
		GD.Print("Hello");
		base.Trigger();
		StateEvents.OnSceneChangeRequested(mainMenuScene);
	}
}
