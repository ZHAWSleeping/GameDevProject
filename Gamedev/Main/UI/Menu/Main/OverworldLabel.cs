using Gamedev.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class OverworldLabel : SelectableLabel
{
	[Export]
	private PackedScene overworldScene;

	public override void Trigger()
	{
		base.Trigger();
		StateEvents.OnSceneChangeRequested(overworldScene);
	}
}
