using Gamedev.Main.Events;
using Godot;
using System;


namespace Gamedev.Main.UI.Debug
{
	public partial class PlayerWallSideLabel : DebugLabel
	{
		public override void _Ready()
		{
			base._Ready();
			DebugEvents.PlayerWallSide += DisplayDirection;
		}
	}
}
