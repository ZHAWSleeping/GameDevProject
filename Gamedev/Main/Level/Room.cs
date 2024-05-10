using Gamedev.Main.Tiles;
using Godot;
using System;

public partial class Room : Node2D
{
	[Export]
	public int Id { get; private set; } = -1;

	[Export]
	public TileMapLayer Tiles;

	[Export]
	private Node2D Boundaries;

	[Export]
	private LoadingZone loadingZone;

	[Export]
	public Node2D Anchor { get; private set; }

	[Export]
	public Node2D RespawnPoint { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Boundaries.QueueFree();
		if (Id == -1)
			GD.PrintErr($"Room \"{Name}\" has invalid ID");
		loadingZone.Id = Id;
	}
}
