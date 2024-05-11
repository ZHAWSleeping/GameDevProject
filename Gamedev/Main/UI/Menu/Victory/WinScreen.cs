using Gamedev.Main.Events;
using Godot;
using System;

public partial class WinScreen : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PersistentEvents.SceneChangeRequested += Disable;
	}


	//TODO Disable pause during menus
	private void Disable(PackedScene packedScene)
	{
		PersistentEvents.SceneChangeRequested -= Disable;
		QueueFree();
	}
}
