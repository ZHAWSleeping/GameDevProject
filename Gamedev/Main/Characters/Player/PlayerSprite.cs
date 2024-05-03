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
		private RemoteTransform2D HeadbandTransform;

		[Export]
		private PinJoint2D Joint;

		private AnimationNodeStateMachinePlayback Animations;


		private bool _flipH = false;
		public bool FlipH
		{
			get { return _flipH; }
			set
			{
				_flipH = value;
				Scale = new(value ? -1 : 1, Scale.Y);
			}
		}

		private bool _flipV = false;
		public bool FlipV
		{
			get { return _flipV; }
			set
			{
				_flipV = value;
				Sprite.FlipV = value;
				Scale = new(Scale.X, value ? -1 : 1);
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
				NodePaths.Headband.Body.GlobalPosition = HeadbandAnchor.GlobalPosition;
				NodePaths.Headband.Body.Freeze = false;
				Joint.NodeB = NodePaths.Headband.Body.GetPath();
				HeadbandTransform.RemotePath = NodePaths.Headband.Sprite.GetPath();
			}
		}

		public void Travel(AnimationState state)
		{
			Animations.Travel(state.ToString());
		}
	}
}
