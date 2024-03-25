using Godot;
using System;


namespace Gamedev.Main.UI
{
	public partial class FPSCounter : Control
	{
		[Export]
		private Label Counter;

		public override void _Ready()
		{
			base._Ready();
			ProcessMode = ProcessModeEnum.Inherit;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			Counter.Text = Performance.GetMonitor(Performance.Monitor.TimeFps).ToString();
		}
	}
}
