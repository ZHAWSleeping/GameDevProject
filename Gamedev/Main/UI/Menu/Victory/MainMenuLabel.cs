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
		base.Trigger();
		StateEvents.OnMainMenuRequest(mainMenuScene);
	}
}
