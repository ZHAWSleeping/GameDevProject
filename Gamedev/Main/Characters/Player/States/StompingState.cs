using System;
using Gamedev.Main.Constants;
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
			if (data.Velocity.Y != data.StompVelocity)
			{
				data.Player.CollisionMask &= ~(uint)Bitmasks.Physics2DLayer.Breakable;
				data.Audio.Play(PlayerAudioManager.Sound.Fall);
			}
			data.Velocity = new(0, data.StompVelocity);
			data.Sprite.Travel(AnimationState.Fall);
		}

		private State GroundedTransition(PlayerData data)
		{
			if (data.Player.IsOnFloor())
			{
				data.Player.CollisionMask |= (uint)Bitmasks.Physics2DLayer.Breakable;
				data.Audio.Stop(PlayerAudioManager.Sound.Fall);
				data.Audio.Play(PlayerAudioManager.Sound.Stomp);
				data.Particles.StompParticlesEmitting = true;
				return State.Grounded;
			}
			return State.Invalid;
		}

	}
}