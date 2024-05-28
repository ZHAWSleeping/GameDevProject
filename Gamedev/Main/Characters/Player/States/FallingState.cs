using System;
using Godot;
using static Gamedev.Main.Characters.Players.PlayerFSM;
using static Gamedev.Main.Characters.Players.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Characters.Players
{
	public class FallingState : PlayerState
	{
		public override State State { get; } = State.Falling;

		protected override Func<PlayerData, State>[] Transitions { get; }

		public FallingState()
		{
			Transitions = new[]
			{
				WallJumpTransition,
				WallTransition,
				GroundedTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			data.Velocity = Move(data, data.MovementSpeed * data.AirborneModifier, data.AirDrag);

			data.Velocity += data.Gravity;
			data.Sprite.Travel(AnimationState.Fall);
		}

		private State WallTransition(PlayerData data)
		{
			if (data.InputDirection.ToQuadrantDirection() == data.WallSide && data.WallSide != Direction.None)
			{
				return State.Wall;
			}
			return State.Invalid;
		}

		private State GroundedTransition(PlayerData data)
		{
			return data.Player.IsOnFloor() ? State.Grounded : State.Invalid;
		}

		private State WallJumpTransition(PlayerData data)
		{
			if (data.JumpJustPressed && data.WallSide != Direction.None)
			{
				data.ResetTimers();
				return State.WallJumping;
			}
			return State.Invalid;
		}

	}
}