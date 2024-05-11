using System;
using Godot;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Events
{
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

		public static void Clear()
		{
			PlayerDied = delegate { };
			PlayerRespawned = delegate { };
		}
	}
}