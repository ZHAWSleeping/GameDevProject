using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Title
{
	public partial class StartGameLabel : SelectableLabel
	{
		[Export]
		private SlidingOverlay Overlay;

		[Export]
		private PackedScene Game;

		public override void _Ready()
		{
			base._Ready();
			RenderingServer.SetDefaultClearColor(Colors.Black);
		}

		public override void Trigger()
		{
			base.Trigger();
			Overlay.Transition(Callable.From(() =>
			{
				CollisionEvents.Clear();
				GameStateEvents.Clear();
				GD.Print(GetTree().ChangeSceneToPacked(Game));
			}));
		}
	}

}