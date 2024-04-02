using Gamedev.Main.Events;
using Godot;
using System;


namespace Gamedev.Main.UI.Debug
{
	public partial class PlayerWallLabel : DebugLabel
	{
		public override void _Ready()
		{
			base._Ready();
			DebugEvents.PlayerWall += DisplayBool;
		}
	}
}
