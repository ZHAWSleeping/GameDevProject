using Gamedev.Main.Events;
using Godot;
using System;

public partial class Goal : Area2D
{
	[Export]
	public PackedScene packedscene;

	private void GoalReached(Node2D body)
	{
		//StateEvents.OnLevelFinished(packedscene);
	}
}
