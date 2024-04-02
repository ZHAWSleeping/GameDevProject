using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class WallCheckerNode : Node2D
{
	[Export]
	WallCheckerArea rightWall;
	[Export]
	WallCheckerArea leftWall;
	// Called when the node enters the scene tree for the first time.

	public bool IsOnAnyWall
	{
		get
		{
			return rightWall.isOnWall || leftWall.isOnWall;
		}
	}

	public bool IsOnlyOnRightWall
	{
		get
		{
			return rightWall.isOnWall && !leftWall.isOnWall;
		}
	}

	public bool IsOnlyOnLeftWall
	{
		get
		{
			return !rightWall.isOnWall && leftWall.isOnWall;
		}
	}
}
