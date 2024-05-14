using Gamedev.Main.Events;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class OverworldMenu : Control
{
	[Export]
	private PackedScene WorldScene;
	[Export]
	private Control Container;
	private IScrollable Scrollable;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Scrollable = (IScrollable)Container;
		PersistentEvents.SaveSelected += UpdateChildren;
	}

	private void UpdateChildren(SaveFile file)
	{
		int i = 0;
		foreach (var world in file.CompletedLevels)
		{
			WorldPanel panel = WorldScene.Instantiate<WorldPanel>();
			panel.UpdateChildren(file, i);
			Scrollable.Instance.AddChild(panel);
			i++;
		}
	}

	private void AnimatedShow()
	{

	}
}
