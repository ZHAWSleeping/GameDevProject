using Gamedev.Events;
using Godot;
using System;

public partial class WinScreen : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StateEvents.SceneChangeRequest += Disable;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//TODO Disable pause during menus
	private void Disable(PackedScene packedScene)
	{
		QueueFree();
	}
}
