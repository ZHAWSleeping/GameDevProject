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

	public bool isOnAnyWall()
	{
		return rightWall.isOnWall || leftWall.isOnWall;
	}

	// return 1 for right and 0 for left
	public int leftOrRight()
	{
		if (isOnAnyWall())
		{
			if (rightWall.isOnWall)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		return -1; // error
	}
}
