using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.UI.Menu
{
	public partial class WinScreen : Control
	{
		private void Disable(PackedScene packedScene)
		{
			QueueFree();
		}
	}
}