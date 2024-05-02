using Godot;
using System;

public partial class Room : Node2D
{
	[Export]
	public TileMapLayer Tiles;

	[Export]
	private Node2D Boundaries;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Boundaries.QueueFree();
	}
}
