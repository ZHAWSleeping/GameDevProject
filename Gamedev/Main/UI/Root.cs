using Gamedev.Events;
using Gamedev.Main.Events;
using Godot;
using System;

public partial class Root : Control
{
	[Export]
	private PackedScene GameOverScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CollisionEvents.CollisionWall += GameOver;
		//RenderingServer.SetDefaultClearColor(Colors.Black);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GameOver() {
		AddChild(GameOverScene.Instantiate());
	}
}
