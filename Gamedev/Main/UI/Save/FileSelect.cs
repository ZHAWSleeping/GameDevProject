using Gamedev.Main.Peristent;
using Godot;
using System;

public partial class FileSelect : HBoxContainer
{
	[Export]
	private PackedScene SaveFilePanelScene;

	[Export]
	private Control SaveFileParent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var save in SaveManager.SaveFiles.Values)
		{
			SaveFilePanel panel = SaveFilePanelScene.Instantiate<SaveFilePanel>();
			panel.File = save;
			SaveFileParent.AddChild(panel);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
