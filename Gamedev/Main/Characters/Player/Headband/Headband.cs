using Gamedev.Events;
using Gamedev.Main.Constants;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

public partial class Headband : Node2D
{
	[Export]
	private Sprite2D NormalSprite;

	[Export]
	private Godot.Collections.Array<RigidBody2D> Bones;

	private List<Dictionary<Direction, List<RigidBody2D>>> BonePairs = new();

	[Export]
	private RigidBody2D HeadbandSprite;

	public override void _Ready()
	{
		NodePaths.Headband = HeadbandSprite;
		//NodePaths.HeadbandBones = GetChildren().OfType<Polygon2D>().SelectMany(body => body.GetChildren().OfType<RigidBody2D>().Take(2)).Select(bone => { bone.Freeze = true; return bone.GetPath(); }).ToArray();
		//NodePaths.HeadbandBones.Append(NormalSprite.GetPath());
		//GetChildren().OfType<Polygon2D>().ToList().ForEach(body =>
		//{
		//	Dictionary<Direction, List<RigidBody2D>> dict = new();
		//	dict[Direction.East] = body.GetChildren().OfType<RigidBody2D>().Take(2).ToList();
		//	dict[Direction.West] = body.GetChildren().OfType<RigidBody2D>().Reverse().Take(2).Reverse().ToList();
		//	BonePairs.Add(dict);
		//	GD.Print(body.Name);
		//});
		//StateEvents.HeadbandAnchorMoved += MoveBones;
	}

	public override void _Process(double delta)
	{
	}

	private void MoveBones(Vector2 anchor, Direction facing)
	{
		NormalSprite.GlobalPosition = anchor;
		if (facing == Direction.West)
		{
			NormalSprite.FlipH = false;
			BonePairs.ForEach((pair) =>
			{
				var i = 0;
				pair[Direction.East].ForEach(bone => {
					bone.Freeze = true;
					bone.GlobalPosition = new(anchor.X + NormalSprite.Texture.GetSize().X, anchor.Y + i);
				});
				pair[Direction.West].ForEach(bone => bone.Freeze = false);
			});
		}
		else
		{
			NormalSprite.FlipH = true;
			BonePairs.ForEach((pair) =>
			{
				var i = 0;
				pair[Direction.West].ForEach(bone => {
					bone.Freeze = true;
					bone.GlobalPosition = new(anchor.X, anchor.Y + i);
				});
				pair[Direction.East].ForEach(bone => bone.Freeze = false);
			});
		}
	}
}
