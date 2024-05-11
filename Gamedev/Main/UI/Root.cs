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
		RenderingServer.SetDefaultClearColor(Colors.Black);
		//StateEvents.LevelFinished += ReplaceScene;
		PersistentEvents.SceneChangeRequested += ReplaceScene;
		PersistentEvents.QuitRequested += QuitGame;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void QuitGame()
	{
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
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
