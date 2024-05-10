using System.Linq;
using Gamedev.Events;
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
			StateEvents.PauseRequested += () => ProcessMode = ProcessModeEnum.Disabled;
			StateEvents.ResumeRequested += () => ProcessMode = ProcessModeEnum.Inherit;
			StateEvents.GameSceneChangeRequested += ReplaceGameScene;
		}

		private void ReplaceGameScene(PackedScene scene)
		{
			CollisionEvents.Clear();
			Viewport.GetChildren().ToList().ForEach(c => c.QueueFree());
			Node2D newScene = scene.Instantiate<Node2D>();
			Viewport.AddChild(newScene);
		}
	}
}