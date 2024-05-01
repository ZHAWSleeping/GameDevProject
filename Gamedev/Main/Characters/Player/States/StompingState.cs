using System;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerFSM;
using static Gamedev.Main.Characters.Player.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Characters.Player
{
	public class StompingState : PlayerState
	{
		public override State State { get; } = State.Stomping;

		protected override Func<PlayerData, State>[] Transitions { get; }

		public StompingState()
		{
			Transitions = new[]
			{
				GroundedTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			data.Velocity = new(0, data.StompVelocity);
			data.Sprite.Travel(AnimationState.Fall);
		}

		private State GroundedTransition(PlayerData data)
		{
			return data.Player.IsOnFloor() ? State.Grounded : State.Invalid;
		}

	}
}