using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
using Godot;
using System;

namespace Gamedev.Main.UI
{
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
			PersistentEvents.QuitRequested += QuitGame;
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
}