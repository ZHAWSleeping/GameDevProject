using System;
using Godot;
using static Gamedev.Main.Characters.Players.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Players.PlayerFSM;

namespace Gamedev.Main.Characters.Players
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
			};
		}

		public override void Execute(PlayerData data)
		{
			data.JumpTime--;

			data.Velocity = Move(data, data.MovementSpeed * data.AirborneModifier, data.AirDrag);

			if (data.PreviousState == State.Grounded)
			{
				data.Velocity = new(data.Velocity.X, -data.JumpVelocity);
				data.Sprite.Travel(AnimationState.Jump);
				data.Particles.JumpParticlesEmitting = true;
				data.Audio.Play(PlayerAudioManager.Sound.Jump);
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