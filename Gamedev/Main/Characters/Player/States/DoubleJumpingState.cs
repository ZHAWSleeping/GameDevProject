using System;
using static Gamedev.Main.Characters.Players.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Players.PlayerFSM;
using Godot;

namespace Gamedev.Main.Characters.Players
{
	public class DoubleJumpingState : PlayerState
	{
		public override State State { get; } = State.DoubleJumping;

		protected override Func<PlayerData, State>[] Transitions { get; }

		public DoubleJumpingState()
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

			if (data.JumpTime == data.JumpTimeFrames - 1)
			{
				data.Velocity = new(data.Velocity.X, -data.JumpVelocity);
				data.Sprite.Travel(AnimationState.Jump);
				data.Particles.JumpParticlesEmitting = true;
				data.Audio.Play(PlayerAudioManager.Sound.DoubleJump);

			}
			else
			{
				data.Velocity = new(data.Velocity.X, data.Velocity.Y - data.JumpVelocityIncrement);
				data.Velocity += data.Gravity;
			}
		}

		private State FallingTransition(PlayerData data)
		{
			return data.JumpTime <= 0
				? State.Falling
				: State.Invalid;
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