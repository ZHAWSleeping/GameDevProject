using Gamedev.Events;
using Gamedev.Main.Extensions;
using Godot;
using System;



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
		public Node2D HeadbandAnchor { get; private set; }

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

		public void Travel(AnimationState state)
		{
			Animations.Travel(state.ToString());
		}
	}
}
