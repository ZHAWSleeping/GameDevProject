using Godot;
using System;

public partial class LevelManager : Node
{

	[Export]
	private Godot.Collections.Array<PackedScene> Scenes { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// select scene from array by index.
	// send signal to load the selected scene.
	public void selectScene(int index)
	{
		if (index < 0)
		{
			GD.Print();
			return;
		}
		// send signal to load scene
	}
}
