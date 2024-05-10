using Gamedev.Main.Characters.Player;
using Gamedev.Main.Tiles;
using Godot;
using System;

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
		Player.GlobalPosition = Merger.Rooms[LevelManager.Instance.Room].RespawnPoint.GlobalPosition;
		Camera.GlobalPosition = Merger.Rooms[LevelManager.Instance.Room].Anchor.GlobalPosition;
		Camera.ResetSmoothing();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
