using Godot;
using System;

namespace Gamedev.Main.Tiles.Destructible
{
	public partial class DestructibleMap : TileMapLayer
	{
		public void Break()
		{
			QueueFree();
		}
	}
}
