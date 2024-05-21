using System;
using Gamedev.Main.Audio;
using Godot;

namespace Gamedev.Main.Events
{
	public static class PersistentAudioEvents
	{
		public static event Action<GlobalAudioManager.Sound> AudioRequested;
		public static void OnAudioRequested(GlobalAudioManager.Sound sound) => AudioRequested(sound);

		static PersistentAudioEvents()
		{
			Clear();
		}

		public static void Clear()
		{
			AudioRequested = delegate { };
		}
	}
	}