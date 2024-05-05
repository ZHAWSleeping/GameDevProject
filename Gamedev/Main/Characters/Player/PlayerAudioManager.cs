using Godot;

namespace Gamedev.Main.Characters.Player
{
	public partial class PlayerAudioManager : Node2D
	{
		public enum Sound
		{
			Jump,
			DoubleJump,
			Dash,
			Fall,
			Stomp,
			Death,
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