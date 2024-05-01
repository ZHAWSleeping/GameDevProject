using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamedev.Main.Tiles
{
	/// <summary>
	/// A TileMapLayer that assumes the TileSet of its first TileMapLayer child and then merges all TileMapLayer children into itself.
	/// </summary>
	public partial class MergingTileMapLayer : TileMapLayer
	{
		public override void _Ready()
		{
			TileSet = GetChildren().OfType<TileMapLayer>().First().TileSet;
			GetChildren().OfType<TileMapLayer>().ToList().ForEach(MergeTilemap);
			GetChildren().ToList().ForEach(n => GD.Print(n.Name));
		}

		private void MergeTilemap(TileMapLayer map)
		{
			Vector2I offset = map.Position.ToVectorI() / TileSet.TileSize;
			foreach (KeyValuePair<Vector2I, TileData> cell in map.GetUsedCells().ToDictionary(pos => pos, pos => map.GetCellTileData(pos)))
			{
				SetCellsTerrainConnect(new Godot.Collections.Array<Vector2I> {cell.Key + offset}, cell.Value.TerrainSet, cell.Value.Terrain);
			}
			map.QueueFree();
		}
	}
}
