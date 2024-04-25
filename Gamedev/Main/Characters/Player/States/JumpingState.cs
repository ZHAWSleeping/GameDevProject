using System;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Player.PlayerFSM;

namespace Gamedev.Main.Characters.Player
{
	public class JumpingState : PlayerState
	{
		public override State State { get; } = State.Jumping;
		protected override Func<PlayerData, State>[] Transitions { get; }

		public JumpingState()
		{
			Transitions = new[]
			{
				WallJumpTransition,
				FallingTransition,
				WallTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			data.JumpTime--;

			// Accelerate
			if (data.InputDirection == Vector2.Zero)
			{
				data.Velocity = new(Mathf.MoveToward(data.Velocity.X, 0, data.MovementSpeed * data.AirborneModifier), data.Velocity.Y);
			}
			else
			{
				data.Velocity = new(data.InputDirection.X * data.MovementSpeed, data.Velocity.Y);
			}

			if (data.PreviousState == State.Grounded)
			{
				data.Velocity = new(data.Velocity.X, -data.JumpVelocity);
				data.Sprite.Travel(AnimationState.Jump);
			}
			else
			{
				data.Velocity = new(data.Velocity.X, data.Velocity.Y - data.JumpVelocityIncrement);
				data.Velocity += data.Gravity;
			}
		}

		private State FallingTransition(PlayerData data)
		{
			return !data.JumpHeld || data.JumpTime <= 0
				? State.Falling
				: State.Invalid;
		}

		private State WallTransition(PlayerData data)
		{
			return data.WallSide != Direction.None && data.WallSide == data.InputDirection.ToQuadrantDirection() ? State.Wall : State.Invalid;
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