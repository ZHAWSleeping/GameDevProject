using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.UI.Debug
{
	public partial class PlayerSpeedLabel : DebugLabel
	{
		public override void _Ready()
		{
			base._Ready();
			DebugEvents.PlayerSpeed += (speed) => DataLabel.Text = $"{speed.X}, {speed.Y}";
		}
	}
}
