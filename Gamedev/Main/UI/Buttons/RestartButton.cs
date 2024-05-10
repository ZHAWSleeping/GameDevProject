using Gamedev.Events;
using Godot;
using System;

public partial class RestartButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ButtonUp += () => StateEvents.OnPlayerDied(Vector2.Zero);
	}
}
