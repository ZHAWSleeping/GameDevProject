using Gamedev.Events;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

public partial class Headband : Node2D
{
	[Export]
	private Sprite2D NormalSprite;

	private List<Dictionary<Direction, List<RigidBody2D>>> BonePairs = new();

	public override void _Ready()
	{
		GetChildren().OfType<Polygon2D>().ToList().ForEach(body => {
			Dictionary<Direction, List<RigidBody2D>> dict = new();
			dict[Direction.East] = body.GetChildren().OfType<RigidBody2D>().Take(2).ToList();
			dict[Direction.West] = body.GetChildren().OfType<RigidBody2D>().Reverse().Take(2).Reverse().ToList();
		});
		//StateEvents.HeadbandAnchorMoved += MoveBones;
	}

	public override void _Process(double delta)
	{
	}

	private void MoveBones(Vector2 anchor, Direction facing)
	{
		
	}
}
