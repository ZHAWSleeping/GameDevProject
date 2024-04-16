using System.Linq;
using Gamedev.Events;
using Godot;

namespace Gamedev.Main.Rendering
{
	public partial class GameViewport : TextureRect
	{
		[Export]
		private SubViewport subviewport;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			StateEvents.PauseRequested += () => ProcessMode = ProcessModeEnum.Disabled;
			StateEvents.ResumeRequested += () => ProcessMode = ProcessModeEnum.Inherit;
			StateEvents.LevelFinished += ReplaceLevel;

		}

		private void ReplaceLevel(PackedScene packedScene)
		{
			subviewport.GetChildren().ToList().First().QueueFree();
			subviewport.AddChild(packedScene.Instantiate());
		}
	}
}