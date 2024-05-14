using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class SelectableDebugRect : TextureRect, Selectable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Modulate = Colors.Green;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Focus()
	{
		Modulate = Colors.Green;
	}

	public void Unfocus()
	{
		Modulate = Colors.Red;
	}

	public void Trigger()
	{
		Modulate = Colors.Blue;
	}
}
