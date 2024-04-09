using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class RayCastEnemy : Node2D
{
	[Export]
	RayCast2D rayFront;
	[Export]
	RayCast2D rayBack;
	[Export]
	RayCast2D rayRight;
	[Export]
	RayCast2D rayLeft;


	public bool groundFront;
	public bool groundBack;
	public bool groundRight;
	public bool groundLeft;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		groundFront = rayFront.IsColliding();
		groundBack = rayBack.IsColliding();

		groundRight = rayRight.IsColliding();
		groundLeft = rayLeft.IsColliding();
		//bool abc = true;
		//((Action)(abc ? () => { groundFront = true; } : () => { })).Invoke();
	}

	public bool NoGap()
	{
		return groundFront && groundBack;
	}

	public bool NoWall()
	{
		return !(groundRight || groundLeft);
	}
}
