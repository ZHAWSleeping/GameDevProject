using Gamedev.Main.Characters.Players;
using Gamedev.Main.Tiles;
using Godot;
using System;

namespace Gamedev.Main.Levels
{
	/// <summary>
	/// Template for all levels. Uses multiple rooms to build a level. 
	/// Has a camera that moves from room to room.
	/// </summary>
	public partial class Level : Node2D
	{
		[Export]
		private Player Player;

		[Export]
		private RoomMerger Merger;

		[Export]
		private Camera2D Camera;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Player.GlobalPosition = Merger.Rooms[LevelManager.Instance.State.CurrentRoom].RespawnPoint.GlobalPosition;
			Camera.GlobalPosition = Merger.Rooms[LevelManager.Instance.State.CurrentRoom].Anchor.GlobalPosition;
			Camera.ResetSmoothing();
		}
	}
}