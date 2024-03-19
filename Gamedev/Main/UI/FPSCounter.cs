using Godot;
using System;


namespace Gamedev.Main.UI
{
    public partial class FPSCounter : Label
    {
        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
			Text = "FPS " + Performance.GetMonitor(Performance.Monitor.TimeFps).ToString();
        }
    }
}
