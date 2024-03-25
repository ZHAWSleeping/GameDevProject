using Gamedev.Events;
using Godot;

namespace Gamedev.Main.Rendering
{
	public partial class GameViewportContainer : SubViewportContainer
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			StateEvents.PauseRequested += () => ProcessMode = ProcessModeEnum.Disabled;
			StateEvents.ResumeRequested += () => ProcessMode = ProcessModeEnum.Inherit;
		}
	}
}