using System.Linq;
using Gamedev.Main.Events;
using Godot;

namespace Gamedev.Main.Rendering
{
	public partial class GameViewport : TextureRect
	{
		[Export]
		private SubViewport Viewport;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			PersistentEvents.PauseRequested += () => ProcessMode = ProcessModeEnum.Disabled;
			PersistentEvents.ResumeRequested += () => ProcessMode = ProcessModeEnum.Inherit;
			PersistentEvents.GameSceneChangeRequested += ReplaceGameScene;
			PersistentEvents.LevelFinished += _ => Clear();
		}

		private void ReplaceGameScene(PackedScene scene)
		{
			Clear();
			Node2D newScene = scene.Instantiate<Node2D>();
			Viewport.AddChild(newScene);
		}

		private void Clear()
		{
			CollisionEvents.Clear();
			GameStateEvents.Clear();
			Viewport.GetChildren().ToList().ForEach(c => c.QueueFree());
		}
	}
}