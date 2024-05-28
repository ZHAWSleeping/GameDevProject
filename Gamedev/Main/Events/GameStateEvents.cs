using System;
using Godot;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Events
{
	/// <summary>
	/// Events that change gamestates such as when the aplyer dies or when he respawns. (stop and resume scenes)
	/// </summary>
	public static class GameStateEvents
	{
		public static event Action<Vector2> PlayerDied;
		public static void OnPlayerDied(Vector2 playerPosition) => PlayerDied(playerPosition);

		public static event Action<Vector2> PlayerRespawned;
		public static void OnPlayerRespawned(Vector2 playerPosition) => PlayerRespawned(playerPosition);

		static GameStateEvents()
		{
			Clear();
		}

		/// <summary>
		/// CLears all signals so that when a new level is loaded no old connections are up which could lead to nullpointers.
		/// </summary>
		public static void Clear()
		{
			PlayerDied = delegate { };
			PlayerRespawned = delegate { };
		}
	}
}