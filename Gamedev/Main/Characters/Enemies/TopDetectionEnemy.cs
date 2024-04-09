using Gamedev.Main.Characters;
using Gamedev.Main.Events;
using Godot;
using System;

public partial class TopDetectionEnemy : RayCast2D
{
	public event Action PlayerJumpedOnTop = delegate { };

	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		if (IsColliding()){
			PlayerJumpedOnTop();
		}
	}
}
