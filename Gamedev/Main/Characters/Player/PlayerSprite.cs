using Godot;
using System;



namespace Gamedev.Main.Characters.Player
{
	public partial class PlayerSprite : Node2D
	{
		[Export]
		public AnimationTree AnimTree { get; private set; }
		[Export]
		private Sprite2D Sprite;

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
	}
}
