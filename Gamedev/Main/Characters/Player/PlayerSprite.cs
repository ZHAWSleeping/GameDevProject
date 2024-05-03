using Gamedev.Events;
using Gamedev.Main.Constants;
using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Linq;



namespace Gamedev.Main.Characters.Player
{
	public partial class PlayerSprite : Node2D
	{

		public enum AnimationState
		{
			Idle,
			Walk,
			Jump,
			Fall,
			Land,
			Wall,
		}

		[Export]
		private AnimationTree AnimTree;

		[Export]
		private AnimatedSprite2D Sprite;

		[Export]
		private Node2D HeadbandAnchor;

		[Export]
		private PinJoint2D Joint;

		private AnimationNodeStateMachinePlayback Animations;


		private bool flipH;
		public bool FlipH
		{
			get { return flipH; }
			set
			{
				flipH = value;
				Sprite.FlipH = value;
			}
		}

		private bool flipV;
		public bool FlipV
		{
			get { return flipV; }
			set
			{
				flipV = value;
				Sprite.FlipV = value;
			}
		}

		public override void _Ready()
		{
			base._Ready();
			Animations = AnimTree.GetStateMachinePlayback();
		}

		public override void _Process(double delta)
		{
			if (NodePaths.Headband != null && Joint.NodeB.IsEmpty)
			{
				NodePaths.Headband.GlobalPosition = HeadbandAnchor.GlobalPosition;
				NodePaths.Headband.Freeze = false;
				Joint.NodeB = NodePaths.Headband.GetPath();
			}
		}

		public void Travel(AnimationState state)
		{
			Animations.Travel(state.ToString());
		}
	}
}
