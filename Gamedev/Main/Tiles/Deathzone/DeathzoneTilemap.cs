using Godot;
using System;

namespace Gamedev.Main.Tiles.Deathzone
{
	public partial class DeathzoneTilemap : TileMap
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Visible = false;
		}
	}
}
