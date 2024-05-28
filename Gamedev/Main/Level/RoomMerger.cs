using Gamedev.Main.Extensions;
using Gamedev.Main.Levels;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamedev.Main.Tiles
{
	/// <summary>
	/// A TileMapLayer that merges the TileMapLayers of all its Room children.
	/// </summary>
	public partial class RoomMerger : TileMapLayer
	{
		public Dictionary<int, Room> Rooms { get; private set; }

		public override void _Ready()
		{
			GetChildren().OfType<Room>().ToList().ForEach(MergeTilemap);
			Rooms = GetChildren().OfType<Room>().ToDictionary(r => r.Id);
		}

		private void MergeTilemap(Room room)
		{
			Vector2I offset = (room.Position + room.Tiles.Position).ToVectorI() / TileSet.TileSize;
			foreach (KeyValuePair<Vector2I, TileData> cell in room.Tiles.GetUsedCells().ToDictionary(pos => pos, pos => room.Tiles.GetCellTileData(pos)))
			{
				SetCellsTerrainConnect(new Godot.Collections.Array<Vector2I> {cell.Key + offset}, cell.Value.TerrainSet, cell.Value.Terrain);
			}
			room.Tiles.QueueFree();
		}
	}
}
