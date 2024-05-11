using Gamedev.Main.Events;
using Godot;

namespace Gamedev.Main.Rendering
{
	public partial class GameViewportContainer : SubViewportContainer
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			PersistentEvents.PauseRequested += () => ProcessMode = ProcessModeEnum.Disabled;
			PersistentEvents.ResumeRequested += () => ProcessMode = ProcessModeEnum.Inherit;
		}
	}
}