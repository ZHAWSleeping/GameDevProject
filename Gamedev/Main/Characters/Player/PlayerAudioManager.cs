using Gamedev.Main.Events;
using Godot;

namespace Gamedev.Main.Characters.Players
{
	public partial class PlayerAudioManager : Node2D
	{
		/// <summary>
		/// Handles the audio for the different actions performed by the player character.
		/// </summary>
		public enum Sound
		{
			Jump,
			DoubleJump,
			Dash,
			Fall,
			Stomp,
			Death,
			CardPickup,
			Victory,
			Break,
		}

		public override void _Ready()
		{
			CollisionEvents.CardCollected += (_) => Play(Sound.CardPickup);
		}

		public void Play(Sound sound)
		{
			AudioStreamPlayer2D stream = GetNodeOrNull<AudioStreamPlayer2D>(sound.ToString());
			if (stream == null)
			{
				GD.PrintErr($"Player {sound} does not exist");
				return;
			}
			stream.Play();
		}

		/// <summary>
		/// Stops a given sound from playing
		/// </summary>
		/// <param name="sound"></param> Sound object that is supposed to be stopped.
		public void Stop(Sound sound)
		{
			AudioStreamPlayer2D stream = GetNodeOrNull<AudioStreamPlayer2D>(sound.ToString());
			if (stream == null)
			{
				GD.PrintErr($"Player {sound} does not exist");
				return;
			}
			stream.Stop();
		}
	}
}