using System;
using static Gamedev.Main.Characters.Players.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Players.PlayerFSM;
using Godot;
using Gamedev.Main.Constants;

namespace Gamedev.Main.Characters.Players
{
	public class DashingState : PlayerState
	{
		public override State State { get; } = State.Dashing;

		protected override Func<PlayerData, State>[] Transitions { get; }

		private float DashDirection = 0.0f;

		public DashingState()
		{
			Transitions = new[]
			{
				WallTransition,
				GroundedTransition,
				FallingTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			if (data.DashTime == data.DashTimeFrames)
			{
				if (data.InputDirection.X != 0)
				{
					DashDirection = MathF.Sign(data.InputDirection.X) * data.DashVelocity;
				}
				else
				{
					DashDirection = MathF.Sign(data.Facing.ToVector().X) * data.DashVelocity;
				}
				data.Particles.DashTrailEmitting = true;
				data.Audio.Play(PlayerAudioManager.Sound.Dash);
				data.Player.CollisionMask &= ~(uint)Bitmasks.Physics2DLayer.Breakable;
			}

			data.DashTime--;
			data.Velocity = new(DashDirection, 0);
			data.Sprite.Travel(AnimationState.Jump);
		}

		private State FallingTransition(PlayerData data)
		{
			if (data.DashTime <= 0)
			{
				data.Player.CollisionMask |= (uint)Bitmasks.Physics2DLayer.Breakable;
				return State.Falling;
			}
			else
			{
				return State.Invalid;
			}
		}

		private State WallTransition(PlayerData data)
		{
			if (data.InputDirection.ToQuadrantDirection() == data.WallSide && data.WallSide != Direction.None && data.DashTime <= 0)
			{
				data.Player.CollisionMask |= (uint)Bitmasks.Physics2DLayer.Breakable;
				return State.Wall;
			}
			return State.Invalid;
		}

		private State GroundedTransition(PlayerData data)
		{
			if (data.Player.IsOnFloor() && data.DashTime <= 0)
			{
				data.Player.CollisionMask |= (uint)Bitmasks.Physics2DLayer.Breakable;
				return State.Grounded;
			}
			else
			{
				return State.Invalid;
			}
		}
	}
}