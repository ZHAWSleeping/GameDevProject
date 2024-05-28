using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Menu
{
	/// <summary>
	/// Label for a button in a menu.
	/// </summary>
	public partial class OverworldLabel : SelectableLabel
	{
		[Export]
		private PackedScene overworldScene;

		public override void Trigger()
		{
			base.Trigger();
		}
	}
}