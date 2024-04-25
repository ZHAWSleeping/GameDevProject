using System;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerFSM;
using static Gamedev.Main.Characters.Player.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Characters.Player
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
			// Slow down and accelerate
			if (data.InputDirection == Vector2.Zero)
			{
				data.Velocity = new(Mathf.MoveToward(data.Velocity.X, 0, data.MovementSpeed * data.AirborneModifier), data.Velocity.Y);
			}
			else
			{
				data.Velocity = new(data.InputDirection.X * data.MovementSpeed * data.AirborneModifier, data.Velocity.Y);
			}

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