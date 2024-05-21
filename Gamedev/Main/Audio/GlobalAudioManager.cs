using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Audio
{
	public partial class GlobalAudioManager : Node
	{
		public enum Sound
		{
			MenuScroll,
			MenuAccept,
			MenuBack,
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			PersistentAudioEvents.AudioRequested += Play;
		}

		public void Play(Sound sound)
		{
			AudioStreamPlayer stream = GetNodeOrNull<AudioStreamPlayer>(sound.ToString());
			if (stream == null)
			{
				GD.PrintErr($"Player {sound} does not exist");
				return;
			}
			stream.Play();
		}

		public void Stop(Sound sound)
		{
			AudioStreamPlayer stream = GetNodeOrNull<AudioStreamPlayer>(sound.ToString());
			if (stream == null)
			{
				GD.PrintErr($"Player {sound} does not exist");
				return;
			}
			stream.Stop();
		}

	}
}
