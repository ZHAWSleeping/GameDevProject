using Gamedev.Events;
using Gamedev.Main.Events;
using Godot;
using System;

public partial class Root : Control
{
	[Export]
	private PackedScene GameOverScene;
	[Export]
	private PackedScene pauseScreen;

	private PackedScene activeScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CollisionEvents.CollisionWall += GameOver;
		RenderingServer.SetDefaultClearColor(Colors.Black);
		StateEvents.LevelFinished += ReplaceScene;
		StateEvents.SceneChangeRequested += ReplaceScene;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GameOver()
	{
		AddChild(GameOverScene.Instantiate());
	}

	private void ReplaceScene(PackedScene packedScene)
	{
		AddChild(packedScene.Instantiate());
	}
}
