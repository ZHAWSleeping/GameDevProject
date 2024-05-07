using Gamedev.Events;
using Godot;
using System;

public partial class OverworldMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StateEvents.SceneChangeRequest += Disable;
	}


	//TODO Disable pause during menus
	private void Disable(PackedScene packedScene)
	{
		StateEvents.SceneChangeRequest -= Disable;
		QueueFree();
	}
}
