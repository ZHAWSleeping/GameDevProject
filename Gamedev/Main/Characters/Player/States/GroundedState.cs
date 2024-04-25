using System;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerFSM;
using static Gamedev.Main.Characters.Player.PlayerSprite;

namespace Gamedev.Main.Characters.Player
{
	public class GroundedState : PlayerState
	{
		public override State State { get; } = State.Grounded;

		protected override Func<PlayerData, State>[] Transitions { get; }

		public GroundedState()
		{
			Transitions = new[]
			{
				JumpingTransition,
				FallingTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			// Reset coyote timer and jump timer or count down coyote
			if (data.Player.IsOnFloor())
			{
				ResetTimers(data);
			}
			else
			{
				data.CoyoteTime--;
			}

			// Slow down and accelerate
			if (data.InputDirection == Vector2.Zero)
			{
				data.Velocity = new(Mathf.MoveToward(data.Player.Velocity.X, 0, data.MovementSpeed), 0.0f);
				data.Sprite.Travel(AnimationState.Idle);
			}
			else
			{
				data.Velocity = new(data.InputDirection.X * data.MovementSpeed, 0.0f);
				data.Sprite.Travel(AnimationState.Walk);
			}

			// Clamp speed
			if (MathF.Abs(data.Velocity.X) > data.MovementSpeed)
			{
				data.Velocity = new(data.MovementSpeed * Mathf.Sign(data.Velocity.X), 0.0f);
			}
		}

		private State JumpingTransition(PlayerData data)
		{
			return data.JumpJustPressed ? State.Jumping : State.Invalid;
		}

		private State FallingTransition(PlayerData data)
		{
			return !data.Player.IsOnFloor() && data.CoyoteTime <= 0
				? State.Falling
				: State.Invalid;
		}

		private void ResetTimers(PlayerData data)
		{
			data.JumpTime = data.JumpTimeFrames;
			data.CoyoteTime = data.CoyoteTimeFrames;
		}
	}
}