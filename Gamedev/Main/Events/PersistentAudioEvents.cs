using System;
using Gamedev.Main.Audio;
using Godot;

namespace Gamedev.Main.Events
{
	/// <summary>
	/// Events that trigger in relation to audio. When audio is trigger for example, jump, dash etc.
	/// </summary>
	public static class PersistentAudioEvents
	{
		public static event Action<GlobalAudioManager.Sound> AudioRequested;
		public static void OnAudioRequested(GlobalAudioManager.Sound sound) => AudioRequested(sound);

		static PersistentAudioEvents()
		{
			Clear();
		}

		/// <summary>
		/// CLears all signals so that when a new level is loaded no old connections are up which could lead to nullpointers.
		/// </summary>
		public static void Clear()
		{
			AudioRequested = delegate { };
		}
	}
	}